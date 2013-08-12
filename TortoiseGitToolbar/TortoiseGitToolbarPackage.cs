﻿using System;
using System.ComponentModel.Design;
using System.Runtime.InteropServices;
using EnvDTE;
using MattDavies.TortoiseGitToolbar.Config.Constants;
using MattDavies.TortoiseGitToolbar.Services;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace MattDavies.TortoiseGitToolbar
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(PackageConstants.guidTortoiseGitToolbarPkgString)]
    [ProvideKeyBindingTable(PackageConstants.guidTortoiseGitToolbarPkgString, 110)]
    public sealed class TortoiseGitToolbarPackage : Package
    {
        private OleMenuCommandService _commandService;
        private TortoiseGitLauncherService _tortoiseGitLauncherService;

        protected override void Initialize()
        {
            base.Initialize();

            _commandService = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            var dte = ((DTE)GetService(typeof(DTE)));
            _tortoiseGitLauncherService = new TortoiseGitLauncherService(dte != null ? dte.Solution : null);
            
            RegisterCommand(ToolbarCommand.Commit, Commit);
            RegisterCommand(ToolbarCommand.Resolve, Resolve);
            RegisterCommand(ToolbarCommand.Push, Push);
            RegisterCommand(ToolbarCommand.Pull, Pull);
            RegisterCommand(ToolbarCommand.Log, Log);
            RegisterCommand(ToolbarCommand.Bash, Bash);
        }

        private void Commit(object sender, EventArgs e)
        {
            _tortoiseGitLauncherService.ExecuteTortoiseProc(ToolbarCommand.Commit.ToString().ToLower());
        }

        private void Resolve(object sender, EventArgs e)
        {
            _tortoiseGitLauncherService.ExecuteTortoiseProc(ToolbarCommand.Resolve.ToString().ToLower());
        }

        private void Push(object sender, EventArgs e)
        {
            _tortoiseGitLauncherService.ExecuteTortoiseProc(ToolbarCommand.Push.ToString().ToLower());
        }

        private void Pull(object sender, EventArgs e)
        {
            _tortoiseGitLauncherService.ExecuteTortoiseProc(ToolbarCommand.Pull.ToString().ToLower());
        }

        private void Log(object sender, EventArgs e)
        {
            _tortoiseGitLauncherService.ExecuteTortoiseProc(ToolbarCommand.Log.ToString().ToLower());
        }

        private void Bash(object sender, EventArgs e)
        {
            _tortoiseGitLauncherService.ExecuteTortoiseProc(ToolbarCommand.Bash.ToString().ToLower());
        }

        private void RegisterCommand(ToolbarCommand id, EventHandler callback)
        {
            var menuCommandID = new CommandID(PackageConstants.guidTortoiseGitToolbarCmdSet, (int)id);
            var menuItem = new OleMenuCommand(callback, menuCommandID);
            _commandService.AddCommand(menuItem);
        }
    }
}
