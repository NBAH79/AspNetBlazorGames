# AspNetBlazorGames
Blazor for games
https://github.com/NBAH79/AspNetBlazorGames/blob/master/2020-08-04%2017-34-27.mp4

1. Create a new Blazor project.</br>
2. Add two services to </br>
<p>public void ConfigureServices(IServiceCollection services)</br>
        {</br>
            ...</br>
            services.AddSingleton<UpdateService>(); </br>
            services.AddSingleton<TankService>();</br>
        }</br>
        </p>
3. Add the razor page "test.razor".</br>
4. Create "img" directory inside wwwroot folder and put tank.png inside </br>
</br>
