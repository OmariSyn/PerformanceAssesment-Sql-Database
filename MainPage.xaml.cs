using Microsoft.Maui.Controls;
using MauiDataApp.Models;
using MauiDataApp.Data;
using System.Collections.ObjectModel;

namespace MauiDataApp
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Item> _items;
        private ItemDatabase _itemDatabase;

        public MainPage()
        {
            InitializeComponent();
            _items = new ObservableCollection<Item>();
            ItemsCollectionView.ItemsSource = _items;

            _itemDatabase = new ItemDatabase();
            LoadItems();
        }

        private async void LoadItems()
        {
            await _itemDatabase.Init();
            var items = await _itemDatabase.GetItemsAsync();
            _items.Clear();
            foreach (var item in items)
            {
                _items.Add(item);
            }
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ItemIdEntry.Text) ||
                string.IsNullOrWhiteSpace(ItemNameEntry.Text) ||
                string.IsNullOrWhiteSpace(ItemDescriptionEntry.Text))
            {
                DisplayAlert("Error", "All fields must be filled out.", "OK").ConfigureAwait(false);
                return;
            }

            var newItem = new Item
            {
                ItemId = int.Parse(ItemIdEntry.Text),
                ItemName = ItemNameEntry.Text,
                ItemDescription = ItemDescriptionEntry.Text
            };

            Task.Run(async () =>
            {
                await _itemDatabase.SaveItemAsync(newItem);
                MainThread.BeginInvokeOnMainThread(() =>
                {
                    LoadItems();

                    // Clear the input fields
                    ItemIdEntry.Text = string.Empty;
                    ItemNameEntry.Text = string.Empty;
                    ItemDescriptionEntry.Text = string.Empty;
                });
            });
        }
    }
}