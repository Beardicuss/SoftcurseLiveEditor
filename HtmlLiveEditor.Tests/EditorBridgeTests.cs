using HtmlLiveEditor.Services;

namespace HtmlLiveEditor.Tests
{
    public class EditorBridgeTests
    {
        private class FakeLogger : IAppLogger
        {
            public void Info(string message) { }
            public void Warn(string message) { }
            public void Error(string message, Exception? ex = null) { }
        }

        [Fact]
        public void IsReady_IsFalse_Initially()
        {
            var bridge = new EditorBridge(new FakeLogger());
            Assert.False(bridge.IsReady);
        }

        [Fact]
        public void Methods_DoNotThrow_WhenWebViewIsNull()
        {
            var bridge = new EditorBridge(new FakeLogger());

            // Should not throw
            var ex1 = Record.Exception(() => bridge.SetContent("<html></html>"));
            var ex2 = Record.Exception(() => bridge.SetTheme("vs-dark"));
            var ex3 = Record.Exception(() => bridge.SetLanguage("css"));
            var ex4 = Record.Exception(() => bridge.InsertSnippet("<div></div>"));
            var ex5 = Record.Exception(() => bridge.HighlightLine(1, "test"));
            var ex6 = Record.Exception(() => bridge.FindAndHighlight("test", "test"));
            var ex7 = Record.Exception(() => bridge.ClearDecorations("test"));

            Assert.Null(ex1);
            Assert.Null(ex2);
            Assert.Null(ex3);
            Assert.Null(ex4);
            Assert.Null(ex5);
            Assert.Null(ex6);
            Assert.Null(ex7);
        }
    }
}
