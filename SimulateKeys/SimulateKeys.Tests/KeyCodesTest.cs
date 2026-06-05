using NUnit.Framework;

namespace SimulateKeys.Tests
{
    // Requires NUnit 2.x (compatible with .NET Compact Framework 3.5)
    // Run on a Windows CE emulator or device with NUnit-CF installed.
    [TestFixture]
    public class KeyCodesTest
    {
        // Control keys
        [Test] public void BackspaceKey() { Assert.AreEqual(0x08, Keys.BACKSPACE); }
        [Test] public void TabKey()       { Assert.AreEqual(0x09, Keys.TAB); }
        [Test] public void EnterKey()     { Assert.AreEqual(0x0D, Keys.ENTER); }
        [Test] public void ShiftKey()     { Assert.AreEqual(0x10, Keys.SHIFT); }
        [Test] public void CtrlKey()      { Assert.AreEqual(0x11, Keys.CTRL); }
        [Test] public void AltKey()       { Assert.AreEqual(0x12, Keys.ALT); }
        [Test] public void EscKey()       { Assert.AreEqual(0x1B, Keys.ESC); }
        [Test] public void SpacebarKey()  { Assert.AreEqual(0x20, Keys.SPACEBAR); }

        // Navigation keys
        [Test] public void HomeKey()      { Assert.AreEqual(0x24, Keys.HOME); }
        [Test] public void EndKey()       { Assert.AreEqual(0x23, Keys.END); }
        [Test] public void PageUpKey()    { Assert.AreEqual(0x21, Keys.PAGEUP); }
        [Test] public void PageDownKey()  { Assert.AreEqual(0x22, Keys.PAGEDOWN); }
        [Test] public void LeftArrow()    { Assert.AreEqual(0x25, Keys.LEFTARROW); }
        [Test] public void RightArrow()   { Assert.AreEqual(0x27, Keys.RIGHTARROW); }
        [Test] public void UpArrow()      { Assert.AreEqual(0x26, Keys.UPARROW); }
        [Test] public void DownArrow()    { Assert.AreEqual(0x28, Keys.DOWNARROW); }

        // Alphanumeric keys
        [Test] public void KeyA()  { Assert.AreEqual(0x41, Keys.KEY_A); }
        [Test] public void KeyZ()  { Assert.AreEqual(0x5A, Keys.KEY_Z); }
        [Test] public void Key0()  { Assert.AreEqual(0x30, Keys.KEY_0); }
        [Test] public void Key9()  { Assert.AreEqual(0x39, Keys.KEY_9); }

        // Function keys
        [Test] public void F1Key()  { Assert.AreEqual(0x70, Keys.F1); }
        [Test] public void F12Key() { Assert.AreEqual(0x7B, Keys.F12); }
        [Test] public void F24Key() { Assert.AreEqual(0x87, Keys.F24); }

        // Keypad
        [Test] public void Keypad0() { Assert.AreEqual(0x60, Keys.KEYPAD_0); }
        [Test] public void Keypad9() { Assert.AreEqual(0x69, Keys.KEYPAD_9); }

        // Verify ascending order of alphanumeric constants (A=0x41, B=0x42, ...)
        [Test]
        public void AlphaKeysAreContiguous()
        {
            Assert.AreEqual(Keys.KEY_A + 25, Keys.KEY_Z);
        }

        // Verify F-key range is contiguous (F1=0x70, F24=0x87)
        [Test]
        public void FunctionKeysAreContiguous()
        {
            Assert.AreEqual(Keys.F1 + 23, Keys.F24);
        }
    }
}
