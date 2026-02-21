using HtmlLiveEditor.Services;

namespace HtmlLiveEditor.Tests
{
    public class AppLoggerTests
    {
        [Fact]
        public void Info_DoesNotThrow()
        {
            var logger = new AppLogger();
            var ex = Record.Exception(() => logger.Info("test message"));
            Assert.Null(ex);
        }

        [Fact]
        public void Warn_DoesNotThrow()
        {
            var logger = new AppLogger();
            var ex = Record.Exception(() => logger.Warn("test warning"));
            Assert.Null(ex);
        }

        [Fact]
        public void Error_WithoutException_DoesNotThrow()
        {
            var logger = new AppLogger();
            var ex = Record.Exception(() => logger.Error("test error"));
            Assert.Null(ex);
        }

        [Fact]
        public void Error_WithException_DoesNotThrow()
        {
            var logger = new AppLogger();
            var ex = Record.Exception(() =>
                logger.Error("test error", new InvalidOperationException("test")));
            Assert.Null(ex);
        }
    }
}
