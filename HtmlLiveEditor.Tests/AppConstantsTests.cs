using HtmlLiveEditor.Config;

namespace HtmlLiveEditor.Tests
{
    public class AppConstantsTests
    {
        [Fact]
        public void EditorDebounceMs_IsReasonable()
        {
            Assert.InRange(AppConstants.EditorDebounceMs, 100, 1000);
        }

        [Fact]
        public void HoverDebounceMs_IsReasonable()
        {
            Assert.InRange(AppConstants.HoverDebounceMs, 30, 300);
        }

        [Fact]
        public void AutoSaveIntervalMs_IsAtLeast10Seconds()
        {
            Assert.True(AppConstants.AutoSaveIntervalMs >= 10_000,
                "Auto-save interval should be at least 10 seconds");
        }

        [Fact]
        public void VirtualHost_IsNotEmpty()
        {
            Assert.False(string.IsNullOrWhiteSpace(AppConstants.VirtualHost));
        }

        [Fact]
        public void EditorUrl_ContainsVirtualHost()
        {
            Assert.Contains(AppConstants.VirtualHost, AppConstants.EditorUrl);
        }

        [Fact]
        public void PreviewUrl_ContainsVirtualHost()
        {
            Assert.Contains(AppConstants.VirtualHost, AppConstants.PreviewUrl);
        }

        [Fact]
        public void LargeFileThreshold_Is1MB()
        {
            Assert.Equal(1_048_576, AppConstants.LargeFileThreshold);
        }

        [Fact]
        public void HtmlFilter_ContainsHtmlExtension()
        {
            Assert.Contains("*.html", AppConstants.HtmlFilter);
        }

        [Fact]
        public void LastFileName_EndsWithHtml()
        {
            Assert.EndsWith(".html", AppConstants.LastFileName);
        }

        [Fact]
        public void HardFileSizeLimit_Is5MB()
        {
            Assert.Equal(5_242_880, AppConstants.HardFileSizeLimit);
        }
    }
}
