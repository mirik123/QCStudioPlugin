New QuantConnect Visual Studio plugin
==============

This Visual Studio plugin allows running QuantConnect backtests either localy using local installation of the QuantConnect Lean engine or remotely by utilizing the QuantConnect REST API.  

## The main plugin window
![QuantConnect client](https://github.com/mirik123/QCStudioPlugin/raw/master/QCStudioPlugin/Resources/QCClient.png "QuantConnect client")

## Local Run
### Specify QuantConnect Lean installation folders
![VisualStudio Options Page](https://github.com/mirik123/QCStudioPlugin/raw/master/QCStudioPlugin/Resources/optionspage.png "VisualStudio Options Page")

### Run workflow
To run backtest locally go to the selected C# file context menu<br/>
![Local run workflow](https://github.com/mirik123/QCStudioPlugin/raw/master/QCStudioPlugin/Resources/runlocal.png "Local run workflow")

And than choose to show results by ZedGraph or by the WebBrowser control.

## Remote Run
To run backtest remotely go to the Visual Studio menu: VIEW -> Other Windows -> QuantConnect Client
![Local run workflow](https://github.com/mirik123/QCStudioPlugin/raw/master/QCStudioPlugin/Resources/runremote.png "Local run workflow")

Then Administrative window pane is opened with sections for managing projects and backtests
![Admin widow](https://github.com/mirik123/QCStudioPlugin/raw/master/QCStudioPlugin/Resources/adminpane.png "Admin widow")


### Managing backtests
![Backtest menu](https://github.com/mirik123/QCStudioPlugin/raw/master/QCStudioPlugin/Resources/BacktestsMenu.png "Backtest menu")

|Menu item name|Menu item description|
|---|---|
|Refresh|Reload the backtests list after selecting active project|
|Load Backtest JS|Load backtest results to chart window (JavaScript WebBrowser)|
|Load Backtest ZED|Load backtest results to chart window (ZedGraph)|
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

### Remote run workflow
After the main window pane is open you can proceed with the choosen scenario:
   1. Login the the system (optional) <br/> *Authentication occures automatically when user sends server request.*
   2. Get projects list with **Resfresh**.
   3. If no projects exist in the cloud:
	   1. Do **Create Project** to create new empty project in cloud.
	   2. Create new local project in Visual Studio or open solution with existing projects. <br/> *It can be actually project of any type. The plugin simply adds to it files from QuantConnect cloud.*
	   3. Connect local project to cloud project.
	   4. Add to the project algorithm files, as described here: <br/>
https://www.quantconnect.com/docs/REST#Developing-in-the-IDE <br/>
All files uploaded to cloud must inherit from QCAlgorithm base type: <br/>````public partial class BasicTemplateAlgorithm : QCAlgorithm, IAlgorithm {...} ````<br/>
The sample algorithm templates are available here: https://github.com/QuantConnect/Lean/tree/master/Algorithm.CSharp
	   5. Create backtest by **Building Project**.
   4. Double click on some project or select a project and do **Refresh backtests**.
   5. Select backtest and do **Load Backtest**.
   6. Do **Backtest Results** to see chart, statistics and trades.
