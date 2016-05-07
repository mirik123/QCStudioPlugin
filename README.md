New QuantConnect Visual Studio plugin
==============

This is a new completely rewritten Visual Studio plugin utilizing the QuantConnect REST API to launch backtests and display results inside Visual Studio 2012-2015.

This plugin consists of 3 parts:
   1. The main VSIX project
   2. The Algorithm project template
   3. The Nuget package that contains Lean binaries necessary for Algorithm project

### The main plugin window

![QuantConnect client](https://github.com/mirik123/QCStudioPlugin/raw/master/QCStudioPlugin/Resources/QCClient.png "QuantConnect client")

### Managing backtests
![Backtest menu](https://github.com/mirik123/QCStudioPlugin/raw/master/QCStudioPlugin/Resources/BacktestsMenu.png "Backtest menu")

|Menu item name|Menu item description|
|---|---|
|Refresh|Reload the backtests list after selecting active project|
|Load Backtest|Load backtest results to chart window|
|Delete Backtest|Delete active backtest|

### Managing projects
![Project menu](https://github.com/mirik123/QCStudioPlugin/raw/master/QCStudioPlugin/Resources/ProjectsMenu.png "Project menu")

|Menu item name|Menu item description|
|---|---|
|Refresh|Reload the projects list. This is first action in workflow, <br/>if user is not authenticated the dialog with email and password pops-up.|
|Create Project|Create new empty project in QuantConnect cloud|
|Delete Project|Delete project from cloud (local project remain untached)|
|Upload Project|Updates cloud project with files from local project connected to it|
|Download Project|Download files from cloud and overwrite local connected project|
|Build Project|Build cloud project and create new backtest|
|Connect Project ID|Connect local project to cloud project. <br/>If local or cloud projects are already connected they should be disconnected first<br/>When local project is connected it is added new property **QCProjectID** with cloud project ID.|
|Disconnect Project ID|Disconnect local project from cloud project|
|Login|Authenticate user|
|Logout|Remove user authentication|

### Getting started (workflow example)
The actions workflow generally looks like that:
   1. Login the the system (optional) <br/> *Authentication occures automatically when user sends server request.*
   2. Get projects list with **Resfresh**.
   3. If no projects exist in the cloud:
	   1. Do **Create Project** to create new empty project in cloud.
	   2. Create new local project in Visual Studio or open solution with existing projects. <br/> *It can be actually project of any type. The plugin simply adds to it files from QuantConnect cloud.*
	   3. Connect local project to cloud project.
	   4. Add to the project classes inherited from QCAlgorithm as described here: https://www.quantconnect.com/terminal
	   5. Create backtest by **Building Project**.
   4. Double click on some project or select a project and do **Refresh backtests**.
   5. Select backtest and do **Load Backtest**.
   6. Do **Backtest Results** to see chart, statistics and trades.

### Features left TODO
   1. Spread components to separate WindowToolPanes:
      1. Charts
      2. Statistics
      3. Administration (Projects/Backtests)
      4. Trades
   2. Create new dialogs to allow Uploading/Downloading only a subset of project files.
   3. Use commercial high-grade framework for drawing stock charts (like SciChart) instead of the ZedGraph.
