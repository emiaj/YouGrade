using StructureMap.Configuration.DSL;
using StructureMap.Pipeline;
using YouGrade.Services;

namespace YouGrade
{
    public class IoCRegistry : Registry
    {
        public IoCRegistry()
        {
            For<IExamTakeContext>().LifecycleIs(new HybridSessionLifecycle()).Use<ExamTakeContext>();
        }        
    }
}