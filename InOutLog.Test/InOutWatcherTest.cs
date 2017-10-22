using Microsoft.VisualStudio.TestTools.UnitTesting;
using InOutLog.Core;
using System.Threading;

namespace InOutLog.Test
{
    [TestClass]
    public class InOutWatcherTest
    {
        private InOutWatcher _watcher;

        [TestInitialize]
        public void Initialize()
        {
            var data = new WatcherData();
            var state = new IdleState(data);
            var persisterMock = (new Moq.Mock<ILogPersister>()).Object;
            var dialogMock = (new Moq.Mock<IDialog>()).Object;
            _watcher = new InOutWatcher(persisterMock, state);
        }

        [TestMethod]
        public void InOutWatcher_Idle()
        {
            //Assert current state
            Assert.IsTrue(_watcher.State is IdleState);

            //Assert data
            Assert.IsTrue(!_watcher.State.Data.StartedAt.HasValue &&
                         !_watcher.State.Data.StoppedAt.HasValue &&
                         !_watcher.State.Data.BreakStartedAt.HasValue &&
                         !_watcher.State.Data.BreakStoppedAt.HasValue &&
                          _watcher.State.Data.CheckInTime.Ticks == 0 &&
                          _watcher.State.Data.TotalBreakInTime.Ticks == 0);

            //Assert behavior
            Assert.IsTrue(_watcher.CanCheckIn &&
                          !_watcher.CanCheckOut &&
                          !_watcher.CanBreakIn &&
                          !_watcher.CanBreakOut);
        }

        [TestMethod]
        public void InOutWatcher_CheckIn()
        {
            //Act
            _watcher.CheckIn();
            Thread.Sleep(100);

            //Assert current state
            Assert.IsTrue(_watcher.State is StartedState);

            //Assert data
            Assert.IsTrue(_watcher.State.Data.StartedAt.HasValue && 
                         !_watcher.State.Data.StoppedAt.HasValue &&
                         !_watcher.State.Data.BreakStartedAt.HasValue &&
                         !_watcher.State.Data.BreakStoppedAt.HasValue &&
                          _watcher.State.Data.CheckInTime.Ticks > 0 &&
                          _watcher.State.Data.TotalBreakInTime.Ticks == 0);

            //Assert behavior
            Assert.IsTrue(!_watcher.CanCheckIn &&
                          _watcher.CanCheckOut &&
                          _watcher.CanBreakIn &&
                          !_watcher.CanBreakOut);
        }

        [TestMethod]
        public void InOutWatcher_CheckOut()
        {
            //Act
            _watcher.CheckIn();
            Thread.Sleep(100);
            _watcher.CheckOut();

            //Assert current state
            Assert.IsTrue(_watcher.State is StoppedState);

            //Assert data
            Assert.IsTrue(_watcher.State.Data.StartedAt.HasValue &&
                         _watcher.State.Data.StoppedAt.HasValue &&
                         !_watcher.State.Data.BreakStartedAt.HasValue &&
                         !_watcher.State.Data.BreakStoppedAt.HasValue &&
                          _watcher.State.Data.CheckInTime.Ticks > 0 &&
                          _watcher.State.Data.TotalBreakInTime.Ticks == 0);

            //Assert behavior
            Assert.IsTrue(!_watcher.CanCheckIn &&
                          !_watcher.CanCheckOut &&
                          !_watcher.CanBreakIn &&
                          !_watcher.CanBreakOut);
        }

        [TestMethod]
        public void InOutWatcher_SingleBreakIn()
        {
            //Act
            _watcher.CheckIn();
            Thread.Sleep(100);
            _watcher.BreakIn();
            Thread.Sleep(100);

            //Assert current state
            Assert.IsTrue(_watcher.State is StartedBreakState);

            //Assert data
            Assert.IsTrue(_watcher.State.Data.StartedAt.HasValue &&
                         !_watcher.State.Data.StoppedAt.HasValue &&
                         _watcher.State.Data.BreakStartedAt.HasValue &&
                         !_watcher.State.Data.BreakStoppedAt.HasValue &&
                          _watcher.State.Data.CheckInTime.Ticks > 0 &&
                          _watcher.State.Data.TotalBreakInTime.Ticks > 0);

            //Assert behavior
            Assert.IsTrue(!_watcher.CanCheckIn &&
                          !_watcher.CanCheckOut &&
                          !_watcher.CanBreakIn &&
                          _watcher.CanBreakOut);
        }

        [TestMethod]
        public void InOutWatcher_SingleBreakOut()
        {
            //Act
            _watcher.CheckIn();
            Thread.Sleep(100);
            _watcher.BreakIn();
            Thread.Sleep(100);
            _watcher.BreakOut();

            //Assert current state
            Assert.IsTrue(_watcher.State is StartedState);

            //Assert data
            Assert.IsTrue(_watcher.State.Data.StartedAt.HasValue &&
                         !_watcher.State.Data.StoppedAt.HasValue &&
                         _watcher.State.Data.BreakStartedAt.HasValue &&
                         _watcher.State.Data.BreakStoppedAt.HasValue &&
                          _watcher.State.Data.CheckInTime.Ticks > 0 &&
                          _watcher.State.Data.TotalBreakInTime.Ticks > 0);

            //Assert behavior
            Assert.IsTrue(!_watcher.CanCheckIn &&
                          _watcher.CanCheckOut &&
                          _watcher.CanBreakIn &&
                          !_watcher.CanBreakOut);
        }

        [TestMethod]
        public void InOutWatcher_MultiBreakIn()
        {
            //Act
            _watcher.CheckIn();
            Thread.Sleep(100);
            _watcher.BreakIn();
            Thread.Sleep(100);
            _watcher.BreakOut();
            Thread.Sleep(100);
            _watcher.BreakIn();

            //Assert current state
            Assert.IsTrue(_watcher.State is StartedBreakState);

            //Assert data
            Assert.IsTrue(_watcher.State.Data.StartedAt.HasValue &&
                         !_watcher.State.Data.StoppedAt.HasValue &&
                         _watcher.State.Data.BreakStartedAt.HasValue &&
                         _watcher.State.Data.BreakStoppedAt.HasValue &&
                          _watcher.State.Data.CheckInTime.Ticks > 0 &&
                          _watcher.State.Data.TotalBreakInTime.Ticks > 0);

            //Assert behavior
            Assert.IsTrue(!_watcher.CanCheckIn &&
                          !_watcher.CanCheckOut &&
                          !_watcher.CanBreakIn &&
                          _watcher.CanBreakOut);
        }

        [TestMethod]
        public void InOutWatcher_MultiBreakOut()
        {
            //Act
            _watcher.CheckIn();
            Thread.Sleep(100);
            _watcher.BreakIn();
            Thread.Sleep(100);
            _watcher.BreakOut();
            Thread.Sleep(100);
            _watcher.BreakIn();
            Thread.Sleep(100);
            _watcher.BreakOut();

            //Assert current state
            Assert.IsTrue(_watcher.State is StartedState);

            //Assert data
            Assert.IsTrue(_watcher.State.Data.StartedAt.HasValue &&
                         !_watcher.State.Data.StoppedAt.HasValue &&
                         _watcher.State.Data.BreakStartedAt.HasValue &&
                         _watcher.State.Data.BreakStoppedAt.HasValue &&
                          _watcher.State.Data.CheckInTime.Ticks > 0 &&
                          _watcher.State.Data.TotalBreakInTime.Ticks > 0);

            //Assert behavior
            Assert.IsTrue(!_watcher.CanCheckIn &&
                          _watcher.CanCheckOut &&
                          _watcher.CanBreakIn &&
                          !_watcher.CanBreakOut);
        }

        [TestMethod]
        public void InOutWatcher_MultiBreakOut_CheckOut()
        {
            //Act
            _watcher.CheckIn();
            Thread.Sleep(100);
            _watcher.BreakIn();
            Thread.Sleep(100);
            _watcher.BreakOut();
            Thread.Sleep(100);
            _watcher.BreakIn();
            Thread.Sleep(100);
            _watcher.BreakOut();
            Thread.Sleep(100);
            _watcher.CheckOut();

            //Assert data
            Assert.IsTrue(_watcher.State.Data.StartedAt.HasValue &&
                         _watcher.State.Data.StoppedAt.HasValue &&
                         _watcher.State.Data.BreakStartedAt.HasValue &&
                         _watcher.State.Data.BreakStoppedAt.HasValue &&
                         _watcher.State.Data.CheckInTime.Ticks > 0 &&
                         _watcher.State.Data.TotalBreakInTime.Ticks > 0);

            //Assert behavior
            Assert.IsTrue(!_watcher.CanCheckIn &&
                          !_watcher.CanCheckOut &&
                          !_watcher.CanBreakIn &&
                          !_watcher.CanBreakOut);
        }
    }
}
