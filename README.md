# Chirp! Web Application
### Description
Chirp! is a razor page application created for the BSc course "Analysis, Design, and Software Architecture" at IT University of Copenhagen.


<a id="terminal"></a>
In the **Terminal/Command Prompt_:

1. Open a new terminal/command prompt at the folder of your choice and type the following command:

```
git clone https://github.com/ITU-BDSA23-GROUP16/Chirp.git
```

2. After the cloning process has been done, navigate into the project directory using

```
cd Chirp
```

You might need to set the correct clientId and clientSecret before running. Use the following commands:

```
dotnet user-secrets init --project src/Chirp.Infrastructure
```

```
dotnet user-secrets set "authentication_github_clientId" "bddd5a8f9b0d7f2c7860" --project src/Chirp.Infrastructure
```

```
dotnet user-secrets set "authentication_github_clientSecret" "1a1d6035b5032e1ae90a4161e331602b037bceec" --project src/Chirp.Infrastructure
```

Run the project by typing the following into the command line:

```
dotnet run --project src/Chirp.Web.
```

3. The program might be able to run and you should be able to follow a link to a localhost instance of the web application through the terminal. It might not work due to trying to connect to the same database as the already running Chirp web app. In that case, you may want to start a Docker container and change the connection string in src/Chirp.Web/Program.cs line 27 to connect to it until you can set up your own Server.

### Run the tests
- Test the program by 
```
dotnet test
```

