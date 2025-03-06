using OldPhonePad;

namespace OldPhoneTests;

public class OldPhoneKeypadTests
{
    private readonly IOldPhoneKeypad _oldPhoneKeyboard;
    
    public OldPhoneKeypadTests()
    {
         _oldPhoneKeyboard = new OldPhoneKeypad();
    }
    
    [Theory]

    [InlineData("33#", "E")]
    [InlineData("227*#", "B")]
    [InlineData("4433555 555666#", "HELLO")]
    [InlineData("8 88777444666*664#", "TURING")]

    public void KeypadOutputMatchesExpectedForGivenInput(string input, string expected)
    {
        var result = _oldPhoneKeyboard.ConvertKeypadInput(input);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void KeypadThrowsExceptionWhenSendKeyNotReceived()
    {
        var input = "8 88777444666*664";
     
        var action = () => _oldPhoneKeyboard.ConvertKeypadInput(input);
       
        Assert.Throws<InvalidOperationException>(action);
    }
}