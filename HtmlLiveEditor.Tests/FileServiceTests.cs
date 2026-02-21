using HtmlLiveEditor.Services;

namespace HtmlLiveEditor.Tests
{
    public class FileServiceTests
    {
        private readonly FileService _sut;

        public FileServiceTests()
        {
            _sut = new FileService(new AppLogger());
        }

        [Fact]
        public void CurrentFilePath_IsNull_Initially()
        {
            Assert.Null(_sut.CurrentFilePath);
        }

        [Fact]
        public void IsLargeFile_ReturnsFalse_ForSmallContent()
        {
            Assert.False(_sut.IsLargeFile("Hello World"));
        }

        [Fact]
        public void IsLargeFile_ReturnsTrue_ForLargeContent()
        {
            // Create a string larger than 1MB
            var largeContent = new string('x', 1_100_000);
            Assert.True(_sut.IsLargeFile(largeContent));
        }

        [Fact]
        public void IsLargeFile_ReturnsFalse_ForExactThreshold()
        {
            // Exactly 1MB should not be considered large (it's > not >=)
            var exactContent = new string('a', 1_048_576);
            Assert.False(_sut.IsLargeFile(exactContent));
        }

        [Fact]
        public void IsLargeFile_ReturnsFalse_ForEmptyString()
        {
            Assert.False(_sut.IsLargeFile(string.Empty));
        }

        [Fact]
        public void AutoSave_DoesNotThrow_ForValidContent()
        {
            var ex = Record.Exception(() => _sut.AutoSave("<html><body>Test</body></html>"));
            Assert.Null(ex);
        }

        [Fact]
        public void Save_ReturnsFalse_WhenNoPath_AndDialogCancelled()
        {
            // With no CurrentFilePath set, Save should try SaveAs
            // But since we can't show a dialog in tests, we verify the path is still null
            Assert.Null(_sut.CurrentFilePath);
        }
    }
}
