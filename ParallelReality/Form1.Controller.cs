/*  Form1.Controller.cs
 *  Version 1.1 (2025.04.17)
 *  
 *  Contributor
 *      Arime-chan (Author)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PromptResult_ = ParallelReality.Form1.PromptResult_;

namespace ParallelReality
{
    public partial class Form1
    {

        private readonly Form1Controller m_Controller = null;
    }

    public class Form1Controller
    {
        public ModManager? ModManager { get => m_ModManager; }

        public bool IsModsApplyingInProgress { get => m_ModManager?.State == ModManagerState.ApplyingMods; }
        public bool IsBaseGameRestoringInProgress { get => m_ModManager?.State == ModManagerState.RestoringBaseGame; }


        public Form1Controller(Form1 _form)
        {
            m_Form = _form;

            m_FileSystemWatcher = new()
            {
                IncludeSubdirectories = true,
                NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName
            };

            m_FileSystemWatcher.Created += HandlerOnModsFolderChanged;
            m_FileSystemWatcher.Deleted += HandlerOnModsFolderChanged;
            m_FileSystemWatcher.Renamed += HandlerOnModsFolderChanged;
        }


        public void Shutdown()
        {
            m_FileSystemWatcher.EnableRaisingEvents = false;
            m_FileSystemWatcher.Dispose();
        }

        public bool InitialCheckBaseGameDir()
        {
            if (!Config.ReadOrCreateNewConfigFile())
            {
                return false;
            }

            if (Config.BaseGameDir == string.Empty)
            {
                Console.WriteLine("BaseGameDir is not configured, prompting user to select BaseGameDir..");

                PromptResult_ result = m_Form.PromptSelectGameExecutable();
                switch (result)
                {
                    case PromptResult_.OK:
                    {
                        Console.WriteLine("    BaseGameDir configured.");
                        _ = Config.SaveConfigFile();
                        return true;
                    }
                    case PromptResult_.UnknownError:
                    {
                        Console.WriteLine("    Unknown Error (this shouldn't happen).");
                        return false;
                    }
                    case PromptResult_.UserCancelled:
                    {
                        Console.WriteLine("    Cancelled. Click 'Select Directory' to try again.");
                        return false;
                    }
                    case PromptResult_.InvalidExecutableLocation:
                    {
                        Console.WriteLine("    Location seems to be invalid. Click 'Select Directory' to try again.");
                        return false;
                    }

                    default:
                        return false;
                }
            }

            return true;
        }

        public void InitializeModManager()
        {
            if (Config.BaseGameDir == string.Empty)
                return;

            m_ModManager = new(Config.BaseGameDir);

            m_ModManager.CheckForDownloadedMods();
            //m_ModManager.CheckForAppliedMods();

            if (!m_ModManager.IsGameModded)
            {
                m_ModManager.BackupBaseGame();
            }

            // =====
            m_FileSystemWatcher.EnableRaisingEvents = false;
            m_FileSystemWatcher.Path = m_ModManager.ModDir;
            m_FileSystemWatcher.EnableRaisingEvents = true;
        }

        public void CancelOperationInProgress(bool _throwOnFirstException = false)
        {
            if (m_CancellationTokenSource == null)
                return;

            m_CancellationTokenSource.Cancel(_throwOnFirstException);
            m_CancellationTokenSource.Dispose();
            m_CancellationTokenSource = null;
        }


        public void DoRestoreBaseGame(Action<string> _callbackStatusUpdate)
        {
            if (m_ModManager == null || m_ModManager.State != ModManagerState.Idle)
                return;

            m_ModManager.RestoreBaseGame(_callbackStatusUpdate);
        }

        public void DoApplyMods(List<int> _selectedMods, Action<string> _callbackStatusUpdate)
        {
            if (m_ModManager == null || m_ModManager.State != ModManagerState.Idle)
                return;

            m_CancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = m_CancellationTokenSource.Token;

            m_ModManager.ApplyMods(_selectedMods, _callbackStatusUpdate, cancellationToken);

            m_CancellationTokenSource.Dispose();
            m_CancellationTokenSource = null;
        }


        private void HandlerOnModsFolderChanged(object sender, FileSystemEventArgs _evt)
        {
            switch (_evt.ChangeType)
            {
                case WatcherChangeTypes.Created:
                    break;
                case WatcherChangeTypes.Deleted:
                    break;
                case WatcherChangeTypes.Renamed:
                    break;
            }

            m_Form.NotifyModsFolderChanged();
        }






        private ModManager? m_ModManager = null;

        private readonly FileSystemWatcher m_FileSystemWatcher;

        private CancellationTokenSource? m_CancellationTokenSource = null;
        private readonly Form1 m_Form;
    }

}
