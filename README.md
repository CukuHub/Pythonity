# Pythonity
Use Python in Unity.

# Add Pythonnet in Unity
1. Install Python as normal
2. Install virtualenv: _**pip install virtualenv**_
3. Open _**Command Prompt**_ and cd in: _**../[ProjectRoot]/Packages**_
4. Create Pythonnet virtualenv: _**virtualenv Pythonnet**_
5. _**cd Pythonnet/Scripts**_
6. Activate the created virtualenv: _**activate.bat**_
7. Install _**Pythonnet**_: _**pip install pythonnet**_
8. Create package file in _**Packages/Pythonnet/package.json**_
9. Add the following in package.json:
```json
{
   "name":"com.cuku.pythonnet",
   "version":"1.0.0",
   "displayName":"Pythonnet",
   "description":"Use Python in Unity.",
   "unity":"2019.4",
   "author":{
      "name":"Cuku"
   },
   "unityRelease":"7f1"
}
```
