using PresentationViewModel;

var vm = new PresentationViewModel.MainView();

vm.LoadData();
Console.WriteLine(vm.DisplayText);