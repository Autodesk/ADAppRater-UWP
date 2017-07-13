# AppRater-UWP

* [General Information](https://git.autodesk.com/zhangmin/ADAppRater-UWP#general-information)
* [Features](https://git.autodesk.com/zhangmin/ADAppRater-UWP#features)
* [Installation](https://git.autodesk.com/zhangmin/ADAppRater-UWP#installation)
* [Criteria and Events](https://git.autodesk.com/zhangmin/ADAppRater-UWP#criteria-and-events)
* [Basic Usage](https://git.autodesk.com/zhangmin/ADAppRater-UWP#basic-usage)
* [Configuration](https://git.autodesk.com/zhangmin/ADAppRater-UWP#configurations)
* [Example Project](https://git.autodesk.com/zhangmin/ADAppRater-UWP#example-project)
* [Special Thanks](https://git.autodesk.com/zhangmin/ADAppRater-UWP#special-thanks)
* [Contact](https://git.autodesk.com/zhangmin/ADAppRater-UWP#contact)

UWP library to easily add an App Rater functionality to your application.
Adding a rater to your app is proven to be an excellent way to gain high marks in UWP store. And even if the user is not 
satisfied, he may send some feedback so you can immidiatly check the issue and fix it.

#### Support
* Built for Visual Studio 2015
* Min UWP SDK version: Windows 10 (10.0; Build 10240)

## General Information
First some general info on how this library works.<br> 

This library will first present the user with an Enjoyment Dialog:<br>
![alt text](https://git.autodesk.com/zhangmin/ADAppRater-UWP/blob/master/Assets/enjoyment.PNG "Enjoyment Dialog")


The user can select No and he will be presented with the Send Feedback Dialog:<br>
![alt text](https://git.autodesk.com/zhangmin/ADAppRater-UWP/blob/master/Assets/contactus.PNG "Contact Us Dialog")

He may select Yes and then be presented with the Rate Us Dialog:<br>
![alt text](https://git.autodesk.com/zhangmin/ADAppRater-UWP/blob/master/Assets/rateus.PNG "Rate Us Dialog")

In the Send Feedback Dialog the user can select Contact us to send a feedback using an email form for instance:<br>
![alt text](https://git.autodesk.com/zhangmin/ADAppRater-UWP/blob/master/Assets/ContactUsMail.PNG "Email")

Or he may simply select No Thanks and the dialog will stop showing.

If the user decided to rate your application, he can click Rate us and he will go to your applications market page.

If he chooses Later, the Rate Dialog will appear after a given amount of time.

## Features
* Target only satisfied users to achieve a higher App Store rating
* Collect valuable feedback and complaints from dissatisfied users
* Easy to define usage parameters to target only experienced users
* Supports multiple scenarios of significant events to target users who have completed a flow

## Installation
For now we only support source code consumption, will add the nuget-package support in the future. Here is the steps to consume this library.
* Download the project, put the project AppRater into you project folder. 
* Add the AppRater project into your solution.
* Right click the Reference, select add reference.
* At the Add Reference page, choose Project page and tick the AppRater.


## Criteria and Events
This library is checking two different types of rules before it shows the first dialog.<br>
The first type is the criteria type. Criteria are threshold rules that must be completed before any other type of rule (events) are checked. In this library there are two of them.<br>

1. Minimum amount of timespan after first time launching the app that must pass before showing the first dialog<br>
2. The app must be online <br>

Only after these two threshold rules are met, there will be a check for the other user provided rules. These user provided rules are called events. They can be anything you choose. A click on a specific button, changing text, opening a file, swiping a drawer.
A user will provide a list of these rules and after the treshold rules are met, the library will check them. Once they are also met, the first dialog will appear.

## Basic Usage
Two simple actions to get things going:<br>
Include the resources in `App.xaml`
```C#
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="ms-appx:///AppRater/Theme/Generic.xaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```
Init the library. Preferably in your `App()` class so you'll have access to the library from any activity.<br>
You can use the default settings and do some initalization:
```C#
void InitAppRater()
{
    AppRater.Criteria.InitFirstTimeLaunchTimestr();
    AppRater.RatingUs.SetRatingUrl("your_product_id_on_uwp_store");
    AppRater.ContactUs.SetFeedbackEmail("your_mailbox_to_receive_feedback@xxx.com");
    AppRater.ContactUs.SetEmailTitle("your mail title");
    AppRater.ContactUs.SetEmailNote("your mail note");
}
```
In the initalization stage, you need set 
* your product id on uwp store 
* the feedback mail
* the feedback mail title
* the feedback mail note

That's it. Now you can simply call `AppRater.Criteria.eventOccurred("EventName");` to report an event has occurred and initiate a rules check. If the rules are satisfied, the Enjoyment Dialog will appear. 
This is the most basic usuage. There are many configurable options. Let's explore some.

## Configurations
In the initialization stage, you can also set the your own event criteria for the AppRater pop up.<br>
```C#
AppRater.Criteria.SetEventCriteria("event1", 10);
AppRater.Criteria.SetEventCriteria("event2", 10);
AppRater.Criteria.SetEventCriteria("event3", 10);

string[,] groups = new string[,] { { "event2", "event3" } };
AppRater.Criteria.GroupEventCriteria (groups);
```
The event criteria means the minimum times this event should happen before we pop up the AppRater.<br>
Your can also group several event criteria into one group, inside this groups they will be connected by `or`, which means if one of the events have the enough occur times to satisfy the criteria, the whole group will be satisfied.

You can some add some additional action to the workflow. For example you may want to add the analystics on the enjoyment dialog pop up.
```C#
void WhenPopUpTheEnjoyment()
{
    Debug.WriteLine("Hello, enjoyment dialog is here!");
    //Do some analytics
}
```
And then in the code, you just hook it to the enjoyment popup by
```C#
AppRater.Workflow.addOnPopUpEnjoymentDialog = new AdditionalAction(WhenPopUpTheEnjoyment);
```
Similarly, you can also provide the additional action on the other dialogs and button clicks.

## Example Project
1. Download project to desktop
2. Open the solution file ‘ADAppRater.sln’
3. The demo app demonstrates 2 ways to use the component:
    * Default UI flow without scenarios
    * Default UI flow, with custom events.

## Special Thanks
* Lian Hau Lee
* David Fei
* Hemant Jaggi
* Amir Shavit

## Contact
* minqi.zhang@autodesk.com

### See Also
* The [GOVERNANCE](GOVERNANCE.md) model
* The [CONTRIBUTING](CONTRIBUTING.md)
