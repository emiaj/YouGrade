using System.Collections.Generic;
using Bottles;
using Bottles.Diagnostics;
using YouGrade.Domain.InMemory.Services;

namespace YouGrade.Domain.InMemory.Bootstrap
{
    public class QuizDataFiller : IActivator
    {
        private readonly InMemoryQuizService _service;

        public QuizDataFiller(InMemoryQuizService service)
        {
            _service = service;
        }

        public void Activate(IEnumerable<IPackageInfo> packages, IPackageLog log)
        {
            var quiz = new Quiz(1, 
                "Learning English with \"The Big Bang Theory\"", "Show your knowledge on the English language using videos from The Big Bang Theory", "en", "http://img.youtube.com/vi/fRaUVp5DfRk/0.jpg");

            var question1 = new Question(1,
                "In this episode, Sheldon proposes to Raj a variation for the classic \"Jokempo\" game: instead of \"rock-paper-scissors\", the variation is \"rock-paper-scissors-lizard-spock\". In his explanation for the game rules, Sheldon points out that paper beats Spock. How can Spock be defeated by paper?", "", "iapcKVn7DdY");

            question1.Alternatives.Add(new Alternative(1,
                                                         "A large sheet of paper can wrap Spock, in the same way that it covers rock.",
                                                         false));
            question1.Alternatives.Add(new Alternative(2,
                                                         "Spock's fingers are not as sharp as scissors, and paper can cut his finger deep.",
                                                         false));

            question1.Alternatives.Add(new Alternative(3,
                                                         "Being Spock a scientist, a paper (academic research) can refute one of his theories.",
                                                         true));

            question1.Alternatives.Add(new Alternative(4,
                                                         "Paper doesn't beat Spock. In fact, Sheldon points out that Spock against paper ends in a tie.",
                                                         false));


            var question2 = new Question(2,
                "In \"The Friendship Algorithm\", Sheldon is learning about friendship and draws a flowchart in his white board. He then invites Kripke to go out with him in some activity. Sheldon uses the friendship algorithm, but every activity proposed by Kripke is rejected by Sheldon, and at some point he gets stuck in an infinite loop. How Howard managed to help him get out of the loop?", "", "w7j7E7J3f6E");


            question2.Alternatives.Add(new Alternative(1,
                                                         "Howard convinced Sheldon to practice rock climbing with Kripke.",
                                                         false));
            question2.Alternatives.Add(new Alternative(2,
                                                         "By drawing a loop counter and an escape to the least objectionable activity.",
                                                         true));

            question2.Alternatives.Add(new Alternative(3,
                                                         "In fact, Howard doesn't help Sheldon at all. Sheldon decides to go rock climbing by itself.",
                                                         true));

            question2.Alternatives.Add(new Alternative(4,
                                                         "Sheldon doesn't get out of the infinite loop. He just get a break to do some internet search before finishing the friendship algorithm path.",
                                                         false));

            var question3 = new Question(3,
                "At the University cafeteria, Sheldon is discussing with his friend Leonard the problem with teleportation. At the end, Leonard seems to agree with Sheldon that there is a problem. What problem they are talking about?", "", "PQZzSrAIp-E");


            question3.Alternatives.Add(new Alternative(1,
                                                         "Both agreed that it would be a problem if the teletransporter had to disintegrate the original Sheldon in order to create the new Sheldon.",
                                                         false));
            question3.Alternatives.Add(new Alternative(2,
                                                         "Both agreed that it would be a problem if the new Sheldon created by the teletransporter had no new improvements compared to the old Sheldon.",
                                                         false));

            question3.Alternatives.Add(new Alternative(3,
                                                         "Sheldon said it would be a problem if the teletransporter had to disintegrate the original Sheldon in order to create the new Sheldon, and Leonard just pretended to care about it.",
                                                         false));

            question3.Alternatives.Add(new Alternative(4,
                                                         "Sheldon said he would never use a teletransporter, because this would disintegrate the original Sheldon in order to create the new Sheldon. Leonard, on the other hand, thought it was a problem if the new Sheldon was exactly the same as the old Sheldon (without improvements), but Sheldon didn't understand Leonard's sarcasm.",
                                                         true));


            var question4 = new Question(4,
                                         "In this video, somebody is auctioning a time machine replica from the \"Time Machine\" movie. Leonard bid $800, and when the auction ends, Sheldon is surprised that no one else bid, since it's a piece of sci-fi movie memorabilia. In the end of the video, Sheldon says he understood why no-one else bid. Why he said so?",
                                         "", "fRaUVp5DfRk");


            question4.Alternatives.Add(new Alternative(1,
                                                         "Because instead of a miniature, Sheldon found the time machine replica to be in fact full-sized and extravagant.",
                                                         true));
            question4.Alternatives.Add(new Alternative(2,
                                                         "Because Sheldon saw it wasn't a real time machine.",
                                                         false));

            question4.Alternatives.Add(new Alternative(3,
                                                         "Because it was nothing like the time machine in the movie.",
                                                         false));

            question4.Alternatives.Add(new Alternative(4,
                                                       "Because the time machine was damaged.",
                                                       true));

            quiz.Questions.Add(question1);
            quiz.Questions.Add(question2);
            quiz.Questions.Add(question3);
            quiz.Questions.Add(question4);

            _service.Add(quiz);
        }
    }
}