﻿using ChessCompanion.MVVM.Model.Utility;
using ChessCompanion.MVVM.ViewModel;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessCompanion.MVVM.Model
{
    class ChessGameTracker
    {
        private readonly GameMediator mediator;

        public ChessGameTracker(GameMediator mediator)
        {
            this.mediator = mediator;
        }
        public void TestFindGame()
        {
            while (true)
            {
                //wait for game to start
                mediator.WaitForResignElement(1000);
                mediator.InitGameScraper();
                if (mediator.PlayingAsWhite())
                {
                    mediator.WaitForFirstMove();
                }
                else
                {
                    mediator.WaitForFirstMove();
                }


                while (mediator.IsResignElementPresent())
                {
                    mediator.WaitForOpponentToMove();
                    //mediator.GetBestMoveWithInfo();
                    mediator.GetBestMoveMultiLines();
                    
                    if (mediator.isEvaluationBarEnabled)
                    {
                        mediator.UpdateEvaluationBar();
                    }
                    if (mediator.isAutoMoveEnabled)
                    {
                        mediator.makeBestMove();
                    }
                    mediator.WaitForPlayerToMove();

                    if (mediator.isAnalysisEnabled)
                    {
                        try
                        {

                            mediator.AnalyzeMove();
                        }
                        catch
                        {
                            break; // exit loop if resign element is no longer present
                        }
                    }

                    if (mediator.isEvaluationBarEnabled)
                    {
                        mediator.GetOpponentTopMove();
                    }

                    Debug.WriteLine("------------");
                }

            }

        }
    }

}

//When everything is stable at some point make the gameloop like this:
//current implemention is only for debugging reasons.
/*
try
{
    while (mediator.IsResignElementPresent())
    {
        // loop body
    }
}
catch (ResignElementException)
{
    // resign element is no longer present, exit loop
}*/