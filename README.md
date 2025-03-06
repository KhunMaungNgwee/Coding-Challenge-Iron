# Coding-Challenge-Iron

# C# Coding Challenge: Old Mobile Phone Keypad Implementation

## Summary

This coding challenge involved implementing the logic of an old Nokia-style mobile phone keypad, where each number key corresponds to multiple letters. Repeated presses of a key cycle through its associated letters. The user must pause (represented by a space in the input) to type consecutive characters from the same key.

Each button has a number to identify it and pressing a button multiple times will cycle through the letters on it allowing each button to represent more than one letter.

*For example, pressing 2 once will return ‘A’ but pressing twice in succession will return ‘B’.*

*A space in the input text is used to simulate the user pausing between button presses.*

You must pause for a second in order to type two characters from the same button after each other: `“222 2 22” -> “CAB”`.

For example:

|  Pressing   |  Inputs   |
|  ---------  | --------- |
|  2          |  A        |
|  22         |  B        |
|  222 2 22   |  CAB      |


## Solution

This solution contains two projects: OldPhoneKeypad, and OldPhoneKeypadTests.

### OldPhone

In this console application you will find the implementation of the business logic wrapped up in a class called *OldPhoneKeypad*. 

As the OldPhoneKeypad method is static, a decision was made to implement this in its own class.  This allows for easier testing of the OldPhoneKeypad and later, should any dependencies be required they could be injected with a DI and a library such as Moq could be used  within the testing project.

The implementation of OldPhoneKeypad is centred around iterating over the characters in the input string, counting successive characters and performing a lookup in the KeyPad dictionary and adding to a String Builder.  

We can also take the appropriate action when the following characters have been detected.


| Character | Key detected | Action |
| --------  | ----------| ------- |
| Space     | Pause  | Move onto the next character   |
| *         | Delete | Remove the last character added to the String Builder     |
| #         | Send   | Return the complete string from the StringBuilder |


### OldPhoneTests

This test project contains a single file that focuses on testing the OldPhoneKeypad class within the OldPhoneKeypadProject - *OldPhoneKeypadTests.cs*.

The *KeypadOutputMatchesExpectedForGivenInput* method uses XUnit's Theory and InlineData to set up multiple tests for the input strings and their expected outputs.  

The *KeypadThrowsExceptionWhenSendKeyNotReceived* method ensure that an InvalidOperationException is thrown when the send key (#) is not appended to the input text.


### Test examples

Here are examples

Assume that a send “#” will always be included at the end of every input.

Examples:
```
OldPhoneKeypad(“33#”) => output: E 
OldPhoneKeypad(“227*#”) => output: B 
OldPhoneKeypad(“4433555 555666#”) => output: HELLO 
OldPhoneKeypad(“8 88777444666*664#”) => output: TURING
```


## Getting Started

### Prerequisites

- .NET 8.0 SDK


### Running the Application
To run the application:
Clone Project first

```
git clone https://github.com/KhunMaungNgwee/Coding-Challenge-Iron.git
cd OldPhoneKeypad
dotnet run
```

Running the project will product the following output:

```
Input: 222 2 22#, translates to: CAB
Input: 33#, translates to: E
Input: 227*#, translates to: B
Input: 4433555 555666#, translates to: HELLO
Input: 8 88777444666*664#, translates to: TURING
Input: 4428883302047773328032999#, translates to: HAVE A GREAT DAY
```

### Running the tests

The tests can be run from the root of the repository using:

```
dotnet test
```
