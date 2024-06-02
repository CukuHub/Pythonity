# Pythonity
Use Python in Unity.

> **Important**: Make sure to call `Pythonity.Shutdown()` after finishing, usually when the application quits.

# Install Pythonnet
1. Install Python as normal
2. Install virtualenv: _**pip install virtualenv**_
3. Open _**Command Prompt**_ and cd to: _**../[ProjectPath]/Packages/Pythonity/Runtime**_
4. Create Pythonnet virtualenv: _**virtualenv Pythonnet**_
5. _**cd Pythonnet/Scripts**_
6. Activate the created virtualenv: _**activate.bat**_
7. Install Pythonnet: _**pip install pythonnet**_

# Create Virtual Environment
1. Open Command Prompt and cd to: _**../[ProjectPath]/Assets/StreamingAssets**_
2. Create virtualenv: _**python -m venv venv**_
3. _**cd venv/Scripts**_
4. Activate the created virtualenv: _**activate.bat**_
5. Install any Python Package that will be used in here
