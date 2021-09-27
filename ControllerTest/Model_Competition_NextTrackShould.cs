using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

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





    }
}
