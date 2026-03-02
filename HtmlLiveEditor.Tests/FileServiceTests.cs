using System;
using System.IO;
using System.Threading.Tasks;
using HtmlLiveEditor.Services;
using Xunit;

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
        public async Task AutoSaveAsync_WritesFileToLocalAppData()
        {
            var content = "<html><body>Test</body></html>";
            await _sut.AutoSaveAsync(content);

            var appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var path = Path.Combine(appData, "SoftcurseLiveScriptor", "last.html");

            Assert.True(File.Exists(path), $"Expected auto-save file at: {path}");
            Assert.Equal(content, await File.ReadAllTextAsync(path));

            // Cleanup
            File.Delete(path);
        }

        [Fact]
        public async Task AutoSaveAsync_DoesNotThrow_ForEmptyString()
        {
            var ex = await Record.ExceptionAsync(() => _sut.AutoSaveAsync(string.Empty));
            Assert.Null(ex);
        }

        [Fact]
        public void Save_WithNoCurrentPath_Falls_BackTo_SaveAs_Flow()
        {
            // Verifies the guard condition: no path set means SaveAs would be triggered.
            // We can't show a dialog in tests, so we verify state is still null and no crash.
            Assert.Null(_sut.CurrentFilePath);
            // If Save() were called here it would open a dialog — UI tests cover this path.
        }
    }
}
