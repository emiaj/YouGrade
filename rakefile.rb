COMPILE_TARGET = ENV['config'].nil? ? "Debug" : ENV['config'] # Keep this in sync w/ VS settings since Mono is case-sensitive
CLR_TOOLS_VERSION = "v4.0.30319"

buildsupportfiles = Dir["#{File.dirname(__FILE__)}/buildsupport/*.rb"]
raise "Run `git submodule update --init` to populate your buildsupport folder." unless buildsupportfiles.any?
buildsupportfiles.each { |ext| load ext }

include FileTest
require 'albacore'
load "VERSION.txt"

RESULTS_DIR = "results"
PRODUCT = "YouGrade"
COPYRIGHT = 'Copyright 2012, Jaime Febres Velez. All rights reserved.';
COMMON_ASSEMBLY_INFO = 'src/CommonAssemblyInfo.cs';
BUILD_DIR = File.expand_path("build")
ARTIFACTS = File.expand_path("artifacts")

@teamcity_build_id = "bt24"
tc_build_number = ENV["BUILD_NUMBER"]
build_revision = tc_build_number || Time.new.strftime('5%H%M')
BUILD_NUMBER = "#{BUILD_VERSION}.#{build_revision}"

props = { :stage => BUILD_DIR, :artifacts => ARTIFACTS }

desc "**Default**, compiles and runs tests"
task :default => [:compile, :unit_test]

desc "Target used for the CI server"
task :ci => [:update_all_dependencies, :default, :history, :package]

desc "Update the version information for the build"
assemblyinfo :version do |asm|
  asm_version = BUILD_VERSION + ".0"
  
  begin
    commit = `git log -1 --pretty=format:%H`
  rescue
    commit = "git unavailable"
  end
  puts "##teamcity[buildNumber '#{BUILD_NUMBER}']" unless tc_build_number.nil?
  puts "Version: #{BUILD_NUMBER}" if tc_build_number.nil?
  asm.trademark = commit
  asm.product_name = PRODUCT
  asm.description = BUILD_NUMBER
  asm.version = asm_version
  asm.file_version = BUILD_NUMBER
  asm.custom_attributes :AssemblyInformationalVersion => asm_version
  asm.copyright = COPYRIGHT
  asm.output_file = COMMON_ASSEMBLY_INFO
end

desc "Prepares the working directory for a new build"
task :clean do
	#TODO: do any other tasks required to clean/prepare the working directory
	FileUtils.rm_rf props[:stage]
    # work around nasty latency issue where folder still exists for a short while after it is removed
    waitfor { !exists?(props[:stage]) }
	Dir.mkdir props[:stage]
    
	Dir.mkdir props[:artifacts] unless exists?(props[:artifacts])
end

def waitfor(&block)
  checks = 0
  until block.call || checks >10 
    sleep 0.5
    checks += 1
  end
  raise 'waitfor timeout expired' if checks > 10
end

desc "Compiles the app"
task :compile => [:restore_if_missing, :clean, :version] do
  MSBuildRunner.compile :compilemode => COMPILE_TARGET, :solutionfile => 'src/YouGrade.sln', :clrversion => CLR_TOOLS_VERSION
  AspNetCompilerRunner.compile :webPhysDir => "src/YouGrade", :webVirDir => "localhost/xyzzyplugh"
  
  copyOutputFiles "src/YouGrade/bin/#{COMPILE_TARGET}", "*.{dll,pdb}", props[:stage]
end

def copyOutputFiles(fromDir, filePattern, outDir)
  Dir.glob(File.join(fromDir, filePattern)){|file| 		
	copy(file, outDir, :preserve => true) if File.file?(file)
  } 
end

desc "Runs unit tests"
task :test => [:unit_test]

desc "Runs unit tests"
task :unit_test => :compile do
  # runner = NUnitRunner.new :compilemode => COMPILE_TARGET, :source => 'src', :platform => 'x86'
  # runner.executeTests ['']
end

desc "Runs the unit tests for Serenity"
task :serenity_test => :compile do
  runner = NUnitRunner.new :compilemode => COMPILE_TARGET, :source => 'src', :platform => 'x86'
  runner.executeTests ['Serenity.Testing']
end


desc "Runs the StoryTeller suite of end to end tests.  IIS must be running first"
task :storyteller => [:compile] do
  storyteller("Storyteller.xml output/st-results.htm")
end

desc "ZIPs up the build results"
zip :package do |zip|
	zip.directories_to_zip = [props[:stage]]
	zip.output_file = 'fubumvc.zip'
	zip.output_path = [props[:artifacts]]
end

def self.bottles(args)
  bottles = Platform.runtime(Nuget.tool("Bottles", "BottleRunner.exe"))
  sh "#{bottles} #{args}"
end

def self.storyteller(args)
st = Platform.runtime(Nuget.tool("Storyteller", "StorytellerRunner.exe")) 
sh "#{st} #{args}"
end

desc "Runs the StoryTeller UI"
task :run_st do
  st = Platform.runtime(Nuget.tool("Storyteller", "StorytellerUI.exe"))
  sh st 
end

desc "Merge missing localization"
task :merge_localization do
	localization("merge src/YouGrade")
end

def self.fubu(args)
  fubu = Platform.runtime("src/fubu/bin/#{COMPILE_TARGET}/fubu.exe")
  sh "#{fubu} #{args}"
end

def self.localization(args)
	localization = Platform.runtime(Nuget.tool("FubuLocalization", "localizer.exe"))
	sh "#{localization} #{args}"
end