using HtmlLiveEditor.Services;

namespace HtmlLiveEditor.Tests
{
    public class PreviewBridgeTests
    {
        private class FakeLogger : IAppLogger
        {
            public void Info(string message) { }
            public void Warn(string message) { }
            public void Error(string message, Exception? ex = null) { }
        }

        [Fact]
        public void UpdateContent_DoesNotThrow_WhenWebViewIsNull()
        {
            var bridge = new PreviewBridge(new FakeLogger());
            var ex = Record.Exception(() => bridge.UpdateContent("<html></html>"));
            Assert.Null(ex);
        }

        [Fact]
        public void SendEditorHover_DoesNotThrow_WhenWebViewIsNull()
        {
            var bridge = new PreviewBridge(new FakeLogger());
            var ex = Record.Exception(() => bridge.SendEditorHover("<div></div>"));
            Assert.Null(ex);
        }

    }
}
