namespace Konscious.Security.Cryptography
{
    using System.Text;
    using Xunit;

    public class Argon2Tests
    {
        static byte[] _password = new byte[] {
            0x5a, 0x28, 0x98, 0xa3, 0x45, 0xc7, 0x20, 0x33, 0x5d, 0x64, 0x39, 0x7b, 0x43, 0xdb, 0xfc, 0x0e,
            0xbe, 0xc8, 0x48, 0x4c, 0x7a, 0x9d, 0xf9, 0xb0, 0xc2, 0xbf, 0x50, 0x74, 0x26, 0x75, 0x3b, 0x58,
            0xc8, 0x38, 0xe8, 0xa4, 0x3f, 0x91, 0xc7, 0x3f, 0x94, 0x3e, 0xa3, 0x75, 0xa6, 0x04, 0xf1, 0x54,
            0x89, 0xeb, 0x12, 0x30, 0x57, 0xbc, 0x6d, 0xd3, 0x47, 0x0f, 0x54, 0x33, 0x84, 0x5a, 0x92, 0xb1,
            0x57, 0xfe, 0xaa, 0x83, 0xcf, 0x1c, 0xc9, 0x0a, 0xd3, 0xd4, 0x7a, 0xa3, 0xd8, 0xbc, 0x12, 0xc6,
            0xb4, 0x2c, 0x89, 0xa0, 0x25, 0x2b, 0x7a, 0x0f, 0xb8, 0x5f, 0xa9, 0xe6, 0x70, 0xae, 0xc7, 0x73,
            0x74, 0xd3, 0xc7, 0x55, 0x2b, 0x9f, 0x86, 0xd8, 0xfb, 0xea, 0x03, 0xea, 0xba, 0x4f, 0x02, 0x8d,
            0x03, 0xc4, 0x75, 0x66, 0xeb, 0x6f, 0x1a, 0xd1, 0x77, 0x25, 0x98, 0x84, 0x2d, 0xd1, 0x8e, 0x00
        };

        static byte[] _salt = new byte[] {
            0xf7, 0x19, 0x2b, 0xa7, 0xff, 0xb8, 0xca, 0xdc, 0x67, 0x51, 0xed, 0xa0, 0x08, 0x1d, 0x9d, 0x95,
            0x0b, 0x10, 0xe4, 0x32, 0x23, 0xef, 0x30, 0x07, 0x39, 0xc6, 0xbc, 0xad, 0x36, 0xda, 0x08, 0xeb,
            0x03, 0x3b, 0xab, 0x98, 0x32, 0x06, 0x7d, 0x39, 0x6f, 0x81, 0x72, 0x24, 0xff, 0x58, 0x41, 0xe6,
            0x33, 0x5d, 0xf7, 0xe7, 0x56, 0xf7, 0xaf, 0x32, 0xfa, 0xd8, 0x72, 0x78, 0xac, 0x63, 0xda, 0xd1
        };

        static byte[] _secret = {
            0xb4, 0xe6, 0x04, 0x41, 0xf6, 0x2d, 0xc4, 0x1a, 0xa0, 0x36, 0x9e, 0x2a, 0xa0, 0xbd, 0x1c, 0xce,
            0x93, 0x1c, 0x8d, 0xb7, 0xb7, 0xaf, 0x11, 0x20, 0xba, 0x5e, 0x99, 0xfc, 0xff, 0xd6, 0xb1, 0x04,
            0x00, 0x55, 0x5b, 0xb0, 0x35, 0x80, 0x43, 0x2e, 0xbf, 0xc7, 0x10, 0x06, 0xe3, 0x04, 0x68, 0xe8,
            0x10, 0xa7, 0x95, 0xb5, 0xd1, 0x02, 0x84, 0x49, 0x4c, 0x22, 0x34, 0x05, 0x90, 0x48, 0x90, 0x4a
        };

        static byte[] _ad = { 0x4b, 0x53, 0x7c, 0xa5, 0xe0, 0x2b, 0xe4, 0x06, 0xce, 0x9e, 0x9e, 0xa3, 0x27, 0x9c, 0x6e, 0x26 };

        [Fact]
        public void TestBigHonkinArgon2iWithEverything()
        {
            var expected = new byte[] {
                0x98, 0x29, 0x12, 0x18, 0x57, 0x65, 0x2b, 0x98, 0x7d, 0x98, 0x8b, 0x68, 0x2c, 0xb0, 0x11, 0xcd,
                0xc5, 0xc6, 0x8a, 0xa0, 0xe9, 0xcb, 0x82, 0xf0, 0xda, 0xd2, 0x46, 0x7a, 0x6d, 0xc5, 0x15, 0x2c,
                0x42, 0x54, 0x52, 0x94, 0x68, 0x12, 0xe7, 0x9f, 0x6e, 0xdb, 0xe0, 0x33, 0x53, 0x3a, 0x21, 0x5f,
                0xe9, 0x97, 0xbc, 0x66, 0xd0, 0x6d, 0x41, 0xe4, 0xea, 0x5e, 0x05, 0x79, 0x97, 0x66, 0x71, 0x79,
                0x75, 0x42, 0x7e, 0xc6, 0xed, 0xe1, 0xf8, 0xa8, 0xef, 0x25, 0xf4, 0xb0, 0xfd, 0xd1, 0x55, 0x86,
                0xdf, 0x9a, 0x18, 0x6a, 0xca, 0x7c, 0x4a, 0x12, 0x9c, 0xd4, 0xe5, 0xc9, 0xf5, 0x70, 0xa7, 0xe5,
                0xd6, 0x40, 0x28, 0x12, 0x08, 0x14, 0xe0, 0x39, 0x65, 0x53, 0x0f, 0xe1, 0xfa, 0x0f, 0x81, 0x1d,
                0xbc, 0xc6, 0x43, 0xdd, 0xff, 0x3a, 0x66, 0x2f, 0xbe, 0x93, 0xf6, 0x43, 0x18, 0xce, 0xe8, 0x38,
                0x82, 0x73, 0x80, 0x8b, 0x09, 0xae, 0xcf, 0x90, 0xd6, 0xc8, 0x63, 0xe0, 0x51, 0x4f, 0x25, 0xbb,
                0x8d, 0xa7, 0x06, 0x32, 0x48, 0xa0, 0xed, 0x3e, 0x6e, 0x52, 0x5a, 0xd9, 0x7d, 0x43, 0x99, 0x09,
                0xc3, 0x69, 0x57, 0x43, 0x64, 0x48, 0x16, 0x79, 0x5d, 0xb4, 0x05, 0x24, 0x08, 0xf4, 0x5e, 0xb7,
                0x70, 0xa5, 0x9a, 0xcc, 0x8d, 0xbe, 0x46, 0xca, 0xa1, 0x7f, 0x02, 0xa7, 0x89, 0xcc, 0x56, 0xe9,
                0x12, 0xa1, 0xb0, 0x0d, 0x41, 0x4d, 0x5f, 0x32, 0xf7, 0x03, 0x20, 0x74, 0x25, 0x20, 0x8a, 0xe0,
                0xf9, 0x86, 0x31, 0xdc, 0x7f, 0xd9, 0xcc, 0x34, 0xaf, 0x6d, 0x51, 0x1b, 0xc7, 0x2a, 0xaf, 0x15,
                0xb4, 0xbb, 0xe5, 0xd6, 0x90, 0xeb, 0x3e, 0xce, 0x64, 0x7c, 0x1b, 0x73, 0x1a, 0x17, 0x43, 0x3f,
                0x04, 0x4e, 0xbf, 0x47, 0x8d, 0x8d, 0x36, 0x0a, 0xe5, 0xb1, 0x39, 0xe0, 0xed, 0x7d, 0x49, 0x37,
                0x9b, 0x51, 0x44, 0x23, 0x45, 0x1d, 0x34, 0x64, 0xed, 0x43, 0x16, 0x56, 0x29, 0x91, 0xcc, 0x8f,
                0xeb, 0x11, 0xa1, 0xd1, 0x24, 0x58, 0xbf, 0x24, 0x0a, 0x70, 0x82, 0xd0, 0x3b, 0x9c, 0x05, 0xde,
                0x1b, 0xbf, 0xe2, 0xa7, 0xa3, 0xcb, 0xad, 0x8e, 0x3b, 0x40, 0xc0, 0xdb, 0x56, 0xca, 0xc4, 0xe2,
                0x06, 0x7a, 0x3f, 0x21, 0x8b, 0x13, 0x31, 0xb9, 0xaa, 0x9f, 0x30, 0x02, 0x5e, 0x04, 0xd5, 0x4f,
                0x2b, 0xca, 0xfe, 0xaa, 0xb8, 0x46, 0xa4, 0x2b, 0xe6, 0xcb, 0x10, 0xb7, 0xb5, 0xbf, 0x0c, 0xd4,
                0xc5, 0x17, 0x38, 0x0e, 0xe1, 0x0c, 0xe9, 0x53, 0x13, 0x7b, 0xf9, 0xbd, 0xf2, 0x33, 0x63, 0x2b,
                0x3d, 0xb4, 0xff, 0x9e, 0xe9, 0x7c, 0xb9, 0x6c, 0x2f, 0xba, 0x77, 0xd3, 0x0a, 0xf8, 0x7a, 0x8d,
                0x2d, 0x76, 0xe3, 0x44, 0x4b, 0x13, 0x1d, 0xbf, 0xe6, 0xf7, 0x3d, 0x73, 0x07, 0x15, 0xca, 0x4a,
                0x30, 0x47, 0xf8, 0xef, 0x6d, 0x78, 0x27, 0xfe, 0xff, 0x6b, 0x34, 0xb7, 0xd0, 0x20, 0x2d, 0xb1,
                0x56, 0x90, 0xf4, 0x6a, 0xb4, 0xe4, 0x16, 0x47, 0xf7, 0xea, 0xc4, 0x70, 0x0e, 0x06, 0xe6, 0xfa,
                0x3a, 0x97, 0xb5, 0xf8, 0x91, 0x48, 0xb6, 0x38, 0x45, 0xa8, 0x28, 0x26, 0x7a, 0x5f, 0x0b, 0x36,
                0xe5, 0xca, 0x9d, 0x19, 0xaf, 0x13, 0x4c, 0x34, 0xdd, 0xdf, 0x55, 0x89, 0x4f, 0x9e, 0xd4, 0x1b,
                0x2e, 0x6d, 0x1c, 0xa7, 0xcd, 0x01, 0xd7, 0xd1, 0x33, 0xbd, 0x45, 0x0c, 0xe8, 0xb6, 0x33, 0x02,
                0xfb, 0xd2, 0x88, 0x5c, 0x3c, 0x01, 0x6a, 0x5e, 0x3d, 0x16, 0x76, 0x6d, 0x04, 0x18, 0x51, 0x28,
                0xf0, 0x1d, 0x49, 0x3a, 0x2f, 0x41, 0xcd, 0xda, 0x94, 0xb7, 0x3a, 0x6a, 0xc1, 0xaa, 0xb1, 0x2d,
                0xd8, 0x58, 0x39, 0x81, 0xcf, 0x74, 0x8a, 0xc8, 0xc4, 0x84, 0xd0, 0xb5, 0x26, 0xcf, 0x5e, 0x03
            };

            var subject = new Argon2i(_password);
            subject.AssociatedData = _ad;
            subject.DegreeOfParallelism = 16;
            subject.Iterations = 15;
            subject.KnownSecret = _secret;
            subject.MemorySize = 4096;
            subject.Salt = _salt;

            var actual = subject.GetBytes(512);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestBigHonkinArgon2dWithEverything()
        {
            var expected = new byte[] {
                0x58, 0x80, 0xed, 0x76, 0xd8, 0xec, 0x7d, 0xf6, 0xdb, 0xf9, 0x33, 0xf1, 0x33, 0x62, 0xb1, 0xfb,
                0xcd, 0xab, 0x12, 0xa1, 0x5e, 0xfe, 0xcf, 0x48, 0xeb, 0x2c, 0xb6, 0xeb, 0xe0, 0x5a, 0x29, 0xe8,
                0xe7, 0x02, 0xe8, 0x54, 0x90, 0x13, 0x31, 0x2a, 0x2a, 0x50, 0xcf, 0x02, 0x08, 0xff, 0x2a, 0x98,
                0x07, 0x76, 0xb9, 0x4d, 0x06, 0x07, 0x6b, 0xd9, 0x83, 0xce, 0x2f, 0x12, 0x2e, 0x6f, 0xc3, 0x94,
                0xf1, 0xac, 0xf6, 0x04, 0x8e, 0x53, 0xcc, 0x70, 0x9c, 0xd4, 0x0d, 0x29, 0xee, 0x29, 0x20, 0x46,
                0x31, 0x4b, 0x01, 0xf0, 0x2e, 0xd2, 0x24, 0xf2, 0x76, 0xf8, 0x14, 0xf9, 0x96, 0x66, 0x57, 0xec,
                0xcf, 0x73, 0xe6, 0xf9, 0x6e, 0x18, 0x3e, 0x0b, 0x38, 0x29, 0x10, 0xae, 0x58, 0x04, 0x44, 0x02,
                0xc9, 0xb3, 0x1e, 0x9c, 0x4a, 0x34, 0x5e, 0x98, 0x28, 0x38, 0xcf, 0xdf, 0x8e, 0x28, 0xdf, 0xe9,
                0xe7, 0x88, 0xad, 0x18, 0x6c, 0xbb, 0x4a, 0xc3, 0x8b, 0x29, 0x04, 0xfb, 0x30, 0xb1, 0x80, 0x4f,
                0xc8, 0xf7, 0x4c, 0x3b, 0xde, 0xa3, 0x5a, 0x25, 0x6e, 0x91, 0x2c, 0x7e, 0xa2, 0x1c, 0x04, 0x72,
                0x28, 0xe5, 0x70, 0x57, 0x53, 0xe7, 0x68, 0x63, 0x5f, 0x1c, 0xb3, 0x49, 0xb1, 0x61, 0x78, 0x40,
                0x51, 0xad, 0xee, 0xab, 0x5c, 0x05, 0xbd, 0x2c, 0x46, 0xb3, 0x07, 0xa1, 0xee, 0xd3, 0x88, 0x9d,
                0x7f, 0xec, 0xde, 0x2c, 0xfc, 0x3d, 0x98, 0x36, 0xa8, 0x2a, 0x24, 0x09, 0x72, 0xcb, 0x73, 0xc2,
                0xff, 0xd7, 0x31, 0xa2, 0x79, 0xea, 0x13, 0x8e, 0xde, 0xcb, 0x46, 0xc6, 0xb6, 0x04, 0x81, 0x7f,
                0xa3, 0x9e, 0xe8, 0xc6, 0xed, 0x69, 0x6f, 0x37, 0x01, 0xa8, 0xa1, 0x8e, 0xf4, 0x0d, 0x5b, 0x09,
                0x68, 0x06, 0xe7, 0xe8, 0x3c, 0xfe, 0x0e, 0xec, 0xd0, 0x67, 0x6a, 0xc2, 0xbb, 0x82, 0x5f, 0x6c,
                0xe2, 0x77, 0x20, 0x2b, 0x4f, 0x4f, 0xd6, 0x41, 0x3f, 0x5c, 0x5d, 0xc2, 0x3b, 0x19, 0x67, 0xc7,
                0x17, 0x64, 0x69, 0x5b, 0x0a, 0x56, 0xd5, 0xdf, 0x1d, 0x23, 0x85, 0xda, 0xa3, 0x86, 0x8d, 0xc6,
                0x96, 0xc4, 0xdb, 0x23, 0xd0, 0x5c, 0x69, 0xcb, 0xfa, 0xe3, 0xda, 0x83, 0x44, 0x7f, 0x1d, 0x49,
                0xb1, 0x97, 0x40, 0x37, 0xf0, 0xa9, 0xdc, 0xf8, 0xe1, 0x74, 0xc0, 0x76, 0x38, 0x1e, 0x75, 0xcf,
                0xf0, 0x7b, 0x5b, 0xe2, 0xa9, 0xe9, 0xa2, 0xbd, 0xc4, 0x3c, 0xf5, 0x71, 0x94, 0x77, 0x27, 0xb7,
                0x36, 0x45, 0xed, 0x75, 0xef, 0x3a, 0x4d, 0xbf, 0x90, 0xed, 0x3c, 0x71, 0x72, 0x6e, 0x7f, 0x3d,
                0x41, 0xb8, 0x1d, 0x53, 0x9c, 0x63, 0xc0, 0xb6, 0x2f, 0xf7, 0xfb, 0x95, 0x5f, 0x07, 0x0a, 0x1f,
                0x82, 0xea, 0xf3, 0xbc, 0x67, 0x2d, 0xf8, 0x0f, 0xb4, 0x0c, 0xac, 0x5e, 0xc7, 0x35, 0x84, 0x53,
                0x9a, 0xff, 0x93, 0xd1, 0xda, 0xa0, 0xf2, 0x6b, 0x41, 0xbc, 0x75, 0x4d, 0x54, 0x86, 0xbe, 0x95,
                0xe7, 0x83, 0x7f, 0x74, 0x8d, 0x2f, 0x84, 0x06, 0x7d, 0xd2, 0x9d, 0x2f, 0x74, 0x14, 0xd9, 0xf9,
                0xc0, 0xdf, 0xee, 0xb6, 0xb6, 0x2e, 0x9d, 0x5c, 0xb9, 0xa1, 0x1f, 0x20, 0x1a, 0xe3, 0x72, 0x67,
                0xce, 0x2c, 0x61, 0xda, 0x15, 0x5b, 0x22, 0xe5, 0xa6, 0x90, 0x84, 0x93, 0x38, 0xf7, 0xa4, 0xa9,
                0x7e, 0xa6, 0xae, 0x57, 0x41, 0x9f, 0x73, 0xf1, 0xe0, 0x9c, 0x7d, 0x14, 0x07, 0xc2, 0xbb, 0xbd,
                0xd6, 0xcb, 0x53, 0xd3, 0xd8, 0x1b, 0x41, 0x2e, 0x02, 0x8b, 0xc3, 0x1f, 0x79, 0xed, 0xd1, 0x4a,
                0x47, 0x55, 0x08, 0xad, 0xe6, 0x0c, 0xfa, 0xde, 0x21, 0x63, 0xc5, 0x91, 0x57, 0x03, 0x90, 0x77,
                0xac, 0x94, 0x5b, 0x00, 0x8e, 0x91, 0x20, 0xde, 0xa7, 0xc7, 0x10, 0x8a, 0xf7, 0x61, 0xbc, 0xfb
            };

            var subject = new Argon2d(_password);
            subject.AssociatedData = _ad;
            subject.DegreeOfParallelism = 16;
            subject.Iterations = 15;
            subject.KnownSecret = _secret;
            subject.MemorySize = 4096;
            subject.Salt = _salt;

            var actual = subject.GetBytes(512);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestArgon2WithAwkwardSize()
        {
            var expected = new byte[] {
                0x67, 0x1e, 0x4f, 0xb6, 0xf9, 0x3c, 0x16, 0xd5, 0x92, 0x1f, 0x33, 0x33, 0x15, 0x54, 0x88, 0xbb,
                0x2f, 0x6e, 0xa9, 0xe1, 0xb3, 0xac, 0x38, 0x26, 0x03, 0x5d, 0x21, 0x0c, 0xd9, 0xd0, 0xe6, 0x86,
                0xb4, 0x1a, 0xf2, 0x00, 0xae, 0x3a, 0xaf, 0x0f, 0xe2, 0xd8, 0xbf, 0xcc, 0xf8, 0xc9, 0xbf, 0xef,
                0x72, 0x61, 0x07, 0x87, 0xb8, 0x8a, 0x54, 0x41, 0x08, 0x68, 0x9a, 0xd8, 0x2a, 0xe7, 0xe3, 0xc5,
                0xe3, 0xc7, 0x2e, 0xc3, 0xd9, 0x42, 0x6f, 0x75, 0x37, 0x97, 0xf6, 0x9b, 0x48, 0x41, 0x29, 0x68,
                0x98, 0xf3, 0x7e, 0x63, 0xe2, 0xca, 0xb5, 0x09, 0x5a, 0xbd, 0xe9, 0x9c, 0xac, 0xfb, 0xc3, 0xa8,
                0x58, 0x97, 0x74, 0xf7, 0x80, 0xfc, 0xe9, 0xed, 0x3e, 0x10, 0x23, 0xac, 0x26, 0xc4, 0x44, 0xb7,
                0xc7, 0x1d, 0xa8, 0xd2, 0x11
            };

            var subject = new Argon2i(_password);
            subject.DegreeOfParallelism = 3;
            subject.Iterations = 4;
            subject.MemorySize = 512;
            subject.Salt = _salt;

            var actual = subject.GetBytes(117);
            Assert.Equal(117, actual.Length);
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void TestArgon2WithShortAwkwardSize()
        {
            var expected = new byte[] {
                0x0c, 0xdc, 0x03, 0xb1, 0x37, 0xe2, 0xae, 0xb4, 0xa0, 0xa5, 0xab, 0x08, 0x85, 0x4d, 0x5d, 0x2c,
                0x3a, 0x5e, 0xaf, 0x42, 0x2d, 0x88, 0x7e, 0x15, 0x38, 0xda, 0xdc, 0xdb, 0x15, 0xdb, 0xfe, 0x24,
                0xa3, 0x29, 0xcb, 0xc4, 0x23, 0xcc, 0x5e, 0xbf, 0xc3, 0x93, 0x60, 0xa0, 0xc8, 0x4e, 0x06
            };

            var subject = new Argon2i(Encoding.UTF8.GetBytes("#S3cr3t+Ke4$"));
            subject.DegreeOfParallelism = 5;
            subject.Iterations = 7;
            subject.MemorySize = 1024;
            subject.Salt = _salt;

            var actual = subject.GetBytes(47);
            Assert.Equal(47, actual.Length);
            Assert.Equal(expected, actual);
        }
    }
}