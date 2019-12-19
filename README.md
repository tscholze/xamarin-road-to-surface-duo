![#rTsd Logo](docs/logo-small.png)

# Road to Surface Duo Xamarin App (#rTsd)
> This is a work in progress [Xamarin.Forms](https://dotnet.microsoft.com/apps/xamarin) Android project which is intended to feature most of the new features that makes the upcoming [Microsoft Surface Duo](https://www.microsoft.com/en-us/surface/devices/surface-duo) phone unique. It will use the awesome German [Dr. Windows](https://www.drwindows.de)  as data source for its articles.

## tl;dr
This is neither an offical [Dr. Windows](https://www.drwindows.de) app nor anything other than a highly motivated playground to dive deep into the Xamarin.Forms ecosystem with a dedicated usecase in mind.

It will target only Android devices and it's meant to include all other aspecs of Xamarin development such as a configurared continuous integration and analytics powerd by the [Azure App Center](https://azure.microsoft.com/en-us/services/app-center/).

This project is written and owned by a beginner and meant for beginners. It would be perfect if experts could mentor the project - but this is just a mind in the clouds.

## Build status

|Service|Status|
|-------|------|
|AppCenter|![Badge](https://build.appcenter.ms/v0.1/apps/b0ad7c4c-b7c3-4fa3-b969-c840e9affc22/branches/master/badge)
|Azure DevOps|[![Build Status](https://dev.azure.com/tscholze/RoadToSurfaceDuo%20Android/_apis/build/status/tscholze.xamarin-road-to-surface-duo?branchName=master)](https://dev.azure.com/tscholze/RoadToSurfaceDuo%20Android/_build/latest?definitionId=9&branchName=master)

## Structure

**rTsd**

Xamarin.Forms container that includes all shared functionality and features.

**rTsd.Android**

Android wrapper and platform-specifc implementations like custom rendereres, effects or assets.

## Phases

- [x] Finding a usecase
- [x] Write down a motivation (e.g. Readme)
- [x] Write an email to `dualscreendev@microsoft.com` to get soon(tm) more information
- [x] Concept roughly an user interface
- [x] Get the project (template) up and running
- [x] Getting into important Xamarin / C# pattern
- [x] Write first MVP code
- [ ] Improve feature set
- [ ] Write tests
- [ ] Leverage CI with tests and test deployment

## Publications

- Personal Blog: [[Projekt] #rTsd – Road to Surface Duo](https://tscholze.uber.space/2019/11/30/projekt-rtsd-road-to-surface-duo/)
- Dr. Windows: [#rTsd – Road to Surface Duo, das Entwicklertagebuch: Teil 1](https://www.drwindows.de/news/rtsd-road-to-surface-duo-das-entwicklertagebuch-teil-1)
- Dr. Windows: [#rTsd – Road to Surface Duo, das Entwicklertagebuch: Teil 2](https://www.drwindows.de/news/road-to-surface-duo-das-entwicklertagebuch-teil-2)
- Dr. Windows: [#rTsd – Road to Surface Duo, das Entwicklertagebuch: Teil 3](https://www.drwindows.de/news/rtsd-road-to-surface-duo-das-entwicklertagebuch-teil-3)

## Prerequirements
- Windows 10 / macOS 
- [Visual Studio 2019](https://visualstudio.microsoft.com/) (for Mac)
- Installed Xamarin Visual Studio plugins

## Assumption

- The Surface Duo will usw two navigation pages, master on the left screen and detail on the right, as the main indicator how to display an app on two screens

## Other programms I used
- Microsoft [OneNote](https://www.onenote.com/) as information storage around the idea of the app
- Microsoft [Teams](https://products.office.com/en-US/microsoft-teams/group-chat-software) to clarify open question with the Dr. Windows team
- [Visual Studio Code](https://code.visualstudio.com/) for all, except source code editing, writings like mark down files
- [Git Bash](https://git-scm.com/downloads) for Git operations outside of Visual Studio
- [Adobe XD](https://www.adobe.com/de/products/xd.html) to protoyp the UI

## Features
- Good looking, hopefully (in my opinion)
- Utilizes Surface Duo features
- Utilizes App Center features
- Lists articles of [Dr. Windows](https://www.drwindows.de) 
- Detail article view
- Share functionality

## UI concepts
I'm not a skilled user interface designer nor I have an eye for beauty, but the app should look mostly like the following concepts.

<center><img src="docs/ui-protoypes.png" height="300" /></center>

## App screenshots
All shown screenshots are taken with version 20191217.

<center><img src="docs/ui-app.png" height="300" /></center>

I used the free, but required an Adobe ID, application [Adobe XD](https://www.adobe.com/de/products/xd.html) to create the protoyp views.

## Decisions made 
- Using the `Page.Navigation(...)` feature instead of the new url-based `Shell.GoToAsync(...)`

## Keep in mind
This app is purely build for having fun! All features have room for improvements or could be done more elegant.

## Contributing
Feel free to improve the quality of the code. It would be great to learn more from experienced C#, Xamarin and Azure developers.
Please use the "default" workflow of contributing to this project.
1. Open an issue and describing your idea or bug finding
1. Fork repository
1. Fix bug / add enhancement in well named feature branch with issue number (e.g. `user.name/2-added-app-icon`)
1. Add the issue number to your commit message (e.g. `#2 Added new app icon`)
1. Create pull request (PR) from your feature branch to this `master` branch 
1. Please keep in touch with your PR to answer upcoming questions
1. If your PR got merged, check if you are listed in the "Authors" section of the readme

## Authors
Just me, [Tobi]([https://tscholze.github.io).

## Links
- [Dr. Windows](https://www.drwindows.de) German Microsoft blog
- [Xamarin](https://dotnet.microsoft.com/apps/xamarin) Homepage


## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
Dependencies or assets maybe licensed differently.
