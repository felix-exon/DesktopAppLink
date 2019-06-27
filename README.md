# DesktopAppLink

With DesktopAppLink any app on your system can be opened from your browser

## [Usage]:

It's fairly simple to use:

```csharp
 DesktopAppLink.CreateLink(
      "applink.sample", 
      "\"<path to exe>\"", 
      "<optionalStaticArgument>");
```

You then create link like this:

```html
<a href="applink.sample:optionalArgumentsFromLink">Run Desktop App</a>
```

### [Arguments]:
There are 2 types of arguments:
- static args (passed to the protocol upon creation)
- arguments that are passed with the protocol when invoking (via webbrowser)
	
#### [Static Arguments]:
Static args are store in the registry of windows next to your protocol. The can only be changed using regedit.

#### [Arguments from Hyperlink]:
Links from hyperlink will always be passed with the protocolname to the application:

<img src="./img/tester.png" >

```html
<a href="mycustomprotocol:origin=web">Click me!</a>
```

<img src="./img/arguments.png" >

## Results:

### Chrome:

<img src="./img/chrome.png" >

### Firefox:

<img src="./img/firefox.png" >
