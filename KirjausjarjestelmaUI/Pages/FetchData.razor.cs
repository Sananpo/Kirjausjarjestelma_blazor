using KirjausjarjestelmaDB.Data.BlazorApp;
using KirjausjarjestelmaUI.Data.BlazorApp;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KirjausjarjestelmaUI.Pages
{
    public partial class FetchData
    {
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        List<FishRecords> fishRecords;
        public bool NewRecord { get; set; } = false;
        protected override async Task OnInitializedAsync()
        {
            var user = (await authenticationStateTask).User;
            fishRecords = await @Service.GetUserFishRecordsAsync(user.Identity.Name);
        }
        FishRecords objFishRecord = new FishRecords();
        bool ShowPopup = false;
        void ClosePopup()
        {
            ShowPopup = false;
        }
        async Task AddNewForecast()
        {
            objFishRecord = new FishRecords();
            objFishRecord.Id = Guid.NewGuid();
            NewRecord = true;
            ShowPopup = true;
            await Task.Delay(1000);

            var dotNetReference = DotNetObjectReference.Create(this);
            await JSRuntime.InvokeVoidAsync("init", dotNetReference);
        }
        async Task SaveForecast()
        {
            ShowPopup = false;
            var user = (await authenticationStateTask).User;
            if (NewRecord == true)
            {
                FishRecords objNewFishRecord = new FishRecords();
                objNewFishRecord.Created = System.DateTime.Now;
                objNewFishRecord.Latitude = objFishRecord.Latitude;
                objNewFishRecord.Longitude = objFishRecord.Longitude;
                objNewFishRecord.Type = objFishRecord.Type;
                objNewFishRecord.Weight = objFishRecord.Weight;
                objNewFishRecord.Length = objFishRecord.Length;
                objNewFishRecord.UserName = user.Identity.Name;

                var result =
                @Service.CreateFishRecordsAsync(objNewFishRecord);
                NewRecord = false;
            }
            else
            {
                var result =
                @Service.UpdateFishRecordsAsync(objFishRecord);
            }

            fishRecords =
            await @Service.GetUserFishRecordsAsync(user.Identity.Name);
        }
        void EditForecast(FishRecords fishRecords)
        {
            objFishRecord = fishRecords;

            ShowPopup = true;
        }
        async Task DeleteForecast()
        {
            ShowPopup = false;
            var user = (await authenticationStateTask).User;
            var result = @Service.DeleteFishRecordsAsync(objFishRecord.Id);
            fishRecords =
            await @Service.GetUserFishRecordsAsync(user.Identity.Name);
        }

        public string LonCoord { get; set; }
        public string LatCoord { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var dotNetReference = DotNetObjectReference.Create(this);
                await JSRuntime.InvokeVoidAsync("init", dotNetReference);
            }
        }

        [JSInvokable("SetCoords")]
        public async Task SetCoords(string Lon, string Lat)
        {
            objFishRecord.Longitude = Lon;
            objFishRecord.Latitude = Lat;
            await JSRuntime.InvokeVoidAsync("addMarker", Lon, Lat);
            StateHasChanged();
        }
    }
}