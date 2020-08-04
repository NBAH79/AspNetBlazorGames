# AspNetBlazorGames
Blazor for games

1. Create a new Blazor project.</br>
2. Add two services to </br>
<p>public void ConfigureServices(IServiceCollection services)</br>
        {</br>
            ...</br>
            services.AddSingleton<UpdateService>(); </br>
            services.AddSingleton<TankService>();</br>
        }</br>
        </p>
3. Add the razor page "test.razor".
4. Create "img" directory inside wwwroot folder and put tank.png inside 

