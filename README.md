# Pythonity
Use Python in Unity.

> [!IMPORTANT]
> Make sure to call `Pythonity.Shutdown()` after finishing! When the application quits for example.

# Install Pythonnet
1. Install Python as normal
2. Install virtualenv: _**pip install virtualenv**_
3. Open Command Prompt at: _**../[ProjectPath]/Packages/Pythonity/Runtime**_
4. Create Pythonnet virtualenv: _**virtualenv Pythonnet**_
5. _**cd Pythonnet/Scripts**_
6. Activate the created virtualenv: _**activate.bat**_
7. Install Pythonnet: _**pip install pythonnet**_
8. Set Api Compatibility Level: _**.NET Framework**_

# Create Virtual Environment
1. Open Command Prompt at: _**../[ProjectPath]/Assets/StreamingAssets**_
2. Create virtualenv: _**python -m venv venv**_
3. _**cd venv/Scripts**_
4. Activate the created virtualenv: _**activate.bat**_
5. Install here any Python Package that will be used within Unity
