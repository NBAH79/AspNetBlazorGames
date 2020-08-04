# AspNetBlazorGames
Blazor for games.</br>
Demo video is <b>demo.mp4</b>

Howto use:
1. Create a new Blazor Server project.</br>
2. Add two services to Startup.cs</br>
<p>public void ConfigureServices(IServiceCollection services)</br>
        {</br>
            ...</br>
            services.AddSingleton<UpdateService>(); </br>
            services.AddSingleton<TankService>();</br>
        }</br>
        </p>
3. Add the razor page "Test.razor".</br>
4. Create "img" directory inside wwwroot folder and put tank.png inside </br>
</br>
