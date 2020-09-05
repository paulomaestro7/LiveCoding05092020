using Live.Coding.Caqui.Consumption.Interface;
using Live.Coding.Caqui.Model;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Employer.LiveCoding2020.ViewModel
{
    public class VotingPageViewModel : ViewModelBase
    {
        private readonly ISatisfaction _satisfaction;

        private int _index;
        public int Index
        {
            get { return _index; }
            set { SetProperty(ref _index, value); }
        }

        private List<SatisfactionModel> _voteParamater;
        public List<SatisfactionModel> VoteParameter
        {
            get { return _voteParamater; }
            set { SetProperty(ref _voteParamater, value); }
        }

        public DelegateCommand VoteCommand
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await Vote();
                });
            }
        }

        public DelegateCommand UpdateVote
        {
            get
            {
                return new DelegateCommand(async () =>
                {
                    await LoadingVote();
                });
            }
        }

        public VotingPageViewModel(ISatisfaction satisfaction, INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            _satisfaction = satisfaction;
            Task.Run(() => LoadingVote());
        }

        public async Task Vote()
        {
            SatisfactionModel satisfaction = new SatisfactionModel();
            string Description = string.Empty;

            switch (Index)
            {
                case 0:
                    Description = "Muito Satisfeito";
                    break;
                case 1:
                    Description = "Satisfeito";
                    break;
                case 2:
                    Description = "Razoavelmente Satisfeito";
                    break;
                case 3:
                    Description = "Pouco Satisfeito";
                    break;
                case 4:
                    Description = "Insatisfeito";
                    break;
            }

            satisfaction.Description = Description;
            satisfaction.HashUser = UserModel.Hash;

            var result = await _satisfaction.PostSatisfaction(satisfaction);
            if (result.Count > 0)
                VoteParameter = result;
            else
                await _pageDialogService.DisplayAlertAsync("Erro", "Nenhum registro encontrado.", "OK");
        }
        public async Task LoadingVote()
        {
            var resultPost = await _satisfaction.GetSatisfaction(UserModel.Hash);

            if (resultPost.Count > 0)
                VoteParameter = resultPost;
            else
                await _pageDialogService.DisplayAlertAsync("Erro", "Nenhum registro encontrado.", "OK");
        }
    }
}
