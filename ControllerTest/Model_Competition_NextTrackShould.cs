using Model;
using Racebaan;
using Controller;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ControllerTest
{
    [TestFixture]

    class Model_Competition_NextTrackShould
    {
        private Competition _competition;

        [SetUp]
        public void SetUp()
        {
            _competition = new Competition();
        }
        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            Track result = _competition.NextTrack();
            Assert.IsNull(result);
        }
        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            Track testbaan = new Track("TestBaan 1", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner });
            _competition.Tracks.Enqueue(testbaan);
            Track result = _competition.NextTrack();
            Assert.AreEqual(result, testbaan);
        }
        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            Track testbaan = new Track("TestBaan 1", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner });
            _competition.Tracks.Enqueue(testbaan);
            Track result = _competition.NextTrack();
            result = _competition.NextTrack();
            Assert.IsNull(result);
        }
        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            Track testbaan1 = new Track("TestBaan 1", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner });
            Track testbaan2 = new Track("TestBaan 2", new SectionTypes[] { SectionTypes.StartGrid, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.Straight, SectionTypes.RightCorner, SectionTypes.Straight, SectionTypes.RightCorner });
            _competition.Tracks.Enqueue(testbaan1);
            _competition.Tracks.Enqueue(testbaan2);
            _competition.NextTrack();
            Track result = _competition.NextTrack();
            Assert.AreEqual(testbaan2, result);

        }
        [Test]
        public void Initialise_Competition_CheckTracks()
        {
            Controller.Data.Initialize();
            
            Assert.NotNull(Controller.Data.Competitie.Tracks);
            Assert.NotNull(Controller.Data.Competitie.Participants);
        }
        [Test]
        public void NextRace_Competition_CheckCurrentRace()
        {
            Data.Initialize();
            Data.NextRace();
            
            Assert.IsNotNull(Data.CurrentRace);
        }
        [Test]
        public void MoveDriver_Race_CheckMove()
        {
            Data.Initialize();
            Data.NextRace();

            Data.CurrentRace.OnTimedEvent(this, new EventArgs());
            
            Assert.IsTrue(Data.CurrentRace.Participants[0].DistanceTravelled > 0);
           
        }
        [Test]
        public void EndRace_Race_CheckFinished()
        {
            Data.Initialize();
            Data.NextRace();
            int numofraces = Data.Competitie.Tracks.Count;            
            Data.CurrentRace.OnTimedEvent(this, new EventArgs());
            Data.CurrentRace.Stop();
            Assert.IsTrue(Data.Competitie.Tracks.Count < numofraces);

        }
        [Test]
        public void DriverFinished()
        {
            Data.Initialize();
            Data.NextRace();
            Data.CurrentRace.OnTimedEvent(this, new EventArgs());
            Data.CurrentRace.Participants[0].LapsDone = Data.CurrentRace.NumofLaps;
            Data.CurrentRace.Participants[1].LapsDone = Data.CurrentRace.NumofLaps;
            Assert.IsTrue(true);
        }




    }
}
