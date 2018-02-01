using System;
using System.IO;
using Xunit;

namespace CSharpCoreTests
{
    public class FilestreamTests
    {
        [Fact(DisplayName = nameof(Filestream_constructor_given_open_mode_and_no_file_throws_error))]
        public void Filestream_constructor_given_open_mode_and_no_file_throws_error()
        {
            Assert.Throws<FileNotFoundException>(() => new FileStream("doesnt_exist.txt", FileMode.Open));
        }

        [Fact(DisplayName = nameof(Filestream_constructor_given_create_new_mode_and_existing_file_throws_error))]
        public void Filestream_constructor_given_create_new_mode_and_existing_file_throws_error()
        {
            Assert.Throws<IOException>(() => new FileStream("already_exists.txt", FileMode.CreateNew));
        }

        [Fact(DisplayName = nameof(Filestream_constructor_given_to_modes_which_combine_to_more_than_append_throw_error))]
        public void Filestream_constructor_given_to_modes_which_combine_to_more_than_append_throw_error()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new FileStream("doesnt_matter", FileMode.CreateNew | FileMode.Append));
        }

        [Fact(DisplayName = nameof(Filestream_construtor_given_two_stream_reading_same_file_throws_error))]
        public void Filestream_construtor_given_two_stream_reading_same_file_throws_error()
        {
            var stream1 = new FileStream("already_exists.txt", FileMode.Open, FileAccess.ReadWrite);
            Assert.Throws<IOException>(() => new FileStream("already_exists.txt", FileMode.Open, FileAccess.ReadWrite));
        }

        [Fact(DisplayName = nameof(Filestream_constructor_given_two_stream_with_read_write_sharing_is_ok))]
        public void Filestream_constructor_given_two_stream_with_read_write_sharing_is_ok()
        {
            var stream1 = new FileStream("read_write.txt", FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
            var stream2 = new FileStream("read_write.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }

        [Fact(DisplayName = nameof(FileMode_or_operator_works_like_expected))]
        public void FileMode_or_operator_works_like_expected()
        {
            Assert.Equal(FileMode.Append, FileMode.Create | FileMode.Append);
        }

        [Fact(DisplayName = nameof(FileMode_or_operator_out_of_range))]
        public void FileMode_or_operator_out_of_range()
        {
            Assert.Equal(7, (int)(FileMode.Append | FileMode.CreateNew));
        }

        [Fact(DisplayName = nameof(Filestream_constructor_given_three_streams_one_writting_with_fileshare_read_and_two_other_reading_is_ok))]
        public void Filestream_constructor_given_three_streams_one_writting_with_fileshare_read_and_two_other_reading_is_ok()
        {
            var stream1 = new FileStream("read.txt", FileMode.Open, FileAccess.Write, FileShare.Read);
            var stream2 = new FileStream("read.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            var stream3 = new FileStream("read.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        }

        [Fact(DisplayName = nameof(Filestream_constructor_given_stream_with_no_read_fileshare_another_read_stream_throws_error))]
        public void Filestream_constructor_given_stream_with_no_read_fileshare_another_read_stream_throws_error()
        {
            var stream1 = new FileStream("write_only.txt", FileMode.Open, FileAccess.Read, FileShare.Write);
            var stream2 = new FileStream("write_only.txt", FileMode.Open, FileAccess.Write, FileShare.ReadWrite);
            Assert.Throws<IOException>(() => new FileStream("write_only.txt", FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
        }
    }
}
