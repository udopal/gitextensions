using System;
using GitCommands.Git;
using GitUIPluginInterfaces;

            var now = DateTime.Now;

            Assert.AreEqual("0 seconds ago", LocalizationHelpers.GetRelativeDateString(now, now));
            Assert.AreEqual("1 second ago", LocalizationHelpers.GetRelativeDateString(now, now.AddSeconds(-1)));
            Assert.AreEqual("1 minute ago", LocalizationHelpers.GetRelativeDateString(now, now.AddMinutes(-1)));
            Assert.AreEqual("1 hour ago", LocalizationHelpers.GetRelativeDateString(now, now.AddMinutes(-45)));
            Assert.AreEqual("1 hour ago", LocalizationHelpers.GetRelativeDateString(now, now.AddHours(-1)));
            Assert.AreEqual("1 day ago", LocalizationHelpers.GetRelativeDateString(now, now.AddDays(-1)));
            Assert.AreEqual("1 week ago", LocalizationHelpers.GetRelativeDateString(now, now.AddDays(-7)));
            Assert.AreEqual("1 month ago", LocalizationHelpers.GetRelativeDateString(now, now.AddDays(-30)));
            Assert.AreEqual("12 months ago", LocalizationHelpers.GetRelativeDateString(now, now.AddDays(-364)));
            Assert.AreEqual("1 year ago", LocalizationHelpers.GetRelativeDateString(now, now.AddDays(-365)));

            Assert.AreEqual("2 seconds ago", LocalizationHelpers.GetRelativeDateString(now, now.AddSeconds(-2)));
            Assert.AreEqual("2 minutes ago", LocalizationHelpers.GetRelativeDateString(now, now.AddMinutes(-2)));
            Assert.AreEqual("2 hours ago", LocalizationHelpers.GetRelativeDateString(now, now.AddHours(-2)));
            Assert.AreEqual("2 days ago", LocalizationHelpers.GetRelativeDateString(now, now.AddDays(-2)));
            Assert.AreEqual("2 weeks ago", LocalizationHelpers.GetRelativeDateString(now, now.AddDays(-14)));
            Assert.AreEqual("2 months ago", LocalizationHelpers.GetRelativeDateString(now, now.AddDays(-60)));
            Assert.AreEqual("2 years ago", LocalizationHelpers.GetRelativeDateString(now, now.AddDays(-730)));

            var now = DateTime.Now;

            Assert.AreEqual("-1 second ago", LocalizationHelpers.GetRelativeDateString(now, now.AddSeconds(1)));
            Assert.AreEqual("-1 minute ago", LocalizationHelpers.GetRelativeDateString(now, now.AddMinutes(1)));
            Assert.AreEqual("-1 hour ago", LocalizationHelpers.GetRelativeDateString(now, now.AddMinutes(45)));
            Assert.AreEqual("-1 hour ago", LocalizationHelpers.GetRelativeDateString(now, now.AddHours(1)));
            Assert.AreEqual("-1 day ago", LocalizationHelpers.GetRelativeDateString(now, now.AddDays(1)));
            Assert.AreEqual("-1 week ago", LocalizationHelpers.GetRelativeDateString(now, now.AddDays(7)));
            Assert.AreEqual("-1 month ago", LocalizationHelpers.GetRelativeDateString(now, now.AddDays(30)));
            Assert.AreEqual("-12 months ago", LocalizationHelpers.GetRelativeDateString(now, now.AddDays(364)));
            Assert.AreEqual("-1 year ago", LocalizationHelpers.GetRelativeDateString(now, now.AddDays(365)));

            Assert.AreEqual("-2 seconds ago", LocalizationHelpers.GetRelativeDateString(now, now.AddSeconds(2)));
            Assert.AreEqual("-2 minutes ago", LocalizationHelpers.GetRelativeDateString(now, now.AddMinutes(2)));
            Assert.AreEqual("-2 hours ago", LocalizationHelpers.GetRelativeDateString(now, now.AddHours(2)));
            Assert.AreEqual("-2 days ago", LocalizationHelpers.GetRelativeDateString(now, now.AddDays(2)));
            Assert.AreEqual("-2 weeks ago", LocalizationHelpers.GetRelativeDateString(now, now.AddDays(14)));
            Assert.AreEqual("-2 months ago", LocalizationHelpers.GetRelativeDateString(now, now.AddDays(60)));
            Assert.AreEqual("-2 years ago", LocalizationHelpers.GetRelativeDateString(now, now.AddDays(730)));
            var module = new GitModule(null);
        public void TestGetDiffChangedFilesFromString()
            var module = new GitModule(null);
                var status = GitCommandHelpers.GetDiffChangedFilesFromString(module, statusString, "HEAD", GitRevision.IndexGuid, "HEAD");
                var status = GitCommandHelpers.GetDiffChangedFilesFromString(module, statusString, "HEAD", GitRevision.IndexGuid, "HEAD");
                var status = GitCommandHelpers.GetDiffChangedFilesFromString(module, statusString, "HEAD", GitRevision.IndexGuid, "HEAD");
                var status = GitCommandHelpers.GetDiffChangedFilesFromString(module, statusString, "HEAD", GitRevision.IndexGuid, "HEAD");
                Assert.IsTrue(status.Count == 1);
                Assert.IsTrue(status[0].Name == "testfile.txt");
            }

            {
                // git diff -M -C -z --cached --name-status
                // Ignore unmerged (in conflict) if revision is work tree
                string statusString = "M  testfile.txt\0U  testfile.txt\0";
                var status = GitCommandHelpers.GetDiffChangedFilesFromString(module, statusString, GitRevision.IndexGuid, GitRevision.UnstagedGuid, GitRevision.IndexGuid);
                Assert.IsTrue(status[0].Staged == StagedStatus.WorkTree);
                // git diff -M -C -z --cached --name-status
                // Include unmerged (in conflict) if revision is index
                string statusString = "M  testfile.txt\0U  testfile2.txt\0";
                var status = GitCommandHelpers.GetDiffChangedFilesFromString(module, statusString, "HEAD", GitRevision.IndexGuid, "HEAD");
                Assert.IsTrue(status[0].Name == "testfile.txt");
                Assert.IsTrue(status[0].Staged == StagedStatus.Index);

            {
                // git diff -M -C -z --name-status 123 456
                // Check that the staged status is None if not Index/WorkTree
                string statusString = "M  testfile.txt\0U  testfile2.txt\0";
                var status = GitCommandHelpers.GetDiffChangedFilesFromString(module, statusString, GitRevision.IndexGuid, "456", "678");
                Assert.IsTrue(status.Count == 2);
                Assert.IsTrue(status[0].Name == "testfile.txt");
                Assert.IsTrue(status[0].Staged == StagedStatus.None);
            }

            {
                // git diff -M -C -z --name-status 123 456
                // Check that the staged status is None if not Index/WorkTree
                string statusString = "M  testfile.txt\0U  testfile2.txt\0";
                var status = GitCommandHelpers.GetDiffChangedFilesFromString(module, statusString, "123", "456", null);
                Assert.IsTrue(status.Count == 2);
                Assert.IsTrue(status[0].Name == "testfile.txt");
                Assert.IsTrue(status[0].Staged == StagedStatus.None);
            }

#if !DEBUG && false
            // This test is for documentation, but as the throw is in a called function, it will not test cleanly
            {
                // git diff -M -C -z --name-status 123 456
                // Check that the staged status is None if not Index/WorkTree
                // Assertion in Debug, throws in Release
                string statusString = "M  testfile.txt\0U  testfile2.txt\0";

                var status = GitCommandHelpers.GetDiffChangedFilesFromString(module, statusString, null, null, null);
                Assert.IsTrue(status.Count == 2);
                Assert.IsTrue(status[0].Name == "testfile.txt");
                Assert.IsTrue(status[0].Staged == StagedStatus.Unknown);
             }
#endif
        public void TestGetStatusChangedFilesFromString()
            var module = new GitModule(null);
            {
                // git status --porcelain=2 --untracked-files=no -z
                // porcelain v1: string statusString = "M  adfs.h\0M  dir.c\0";
                string statusString = "#Header\03 unknown info\01 .M S..U 160000 160000 160000 cbca134e29be13b35f21ca4553ba04f796324b1c cbca134e29be13b35f21ca4553ba04f796324b1c adfs.h\01 .M SCM. 160000 160000 160000 6bd3b036fc5718a51a0d27cde134c7019798c3ce 6bd3b036fc5718a51a0d27cde134c7019798c3ce dir.c\0\r\nwarning: LF will be replaced by CRLF in adfs.h.\nThe file will have its original line endings in your working directory.\nwarning: LF will be replaced by CRLF in dir.c.\nThe file will have its original line endings in your working directory.";
                var status = GitCommandHelpers.GetStatusChangedFilesFromString(module, statusString);
                Assert.IsTrue(status.Count == 2);
                Assert.IsTrue(status[0].Name == "adfs.h");
                Assert.IsTrue(status[1].Name == "dir.c");
            }

            {
                // git status --porcelain=2 --untracked-files -z
                // porcelain v1: string statusString = "M  adfs.h\0?? untracked_file\0";
                string statusString = "1 .M S..U 160000 160000 160000 cbca134e29be13b35f21ca4553ba04f796324b1c cbca134e29be13b35f21ca4553ba04f796324b1c adfs.h\0? untracked_file\0";
                var status = GitCommandHelpers.GetStatusChangedFilesFromString(module, statusString);
                Assert.IsTrue(status.Count == 2);
                Assert.IsTrue(status[0].Name == "adfs.h");
                Assert.IsTrue(status[1].Name == "untracked_file");
            }

            {
                // git status --porcelain=2 --ignored-files -z
                // porcelain v1: string statusString = ".M  adfs.h\0!! ignored_file\0";
                string statusString = "1 .M S..U 160000 160000 160000 cbca134e29be13b35f21ca4553ba04f796324b1c cbca134e29be13b35f21ca4553ba04f796324b1c adfs.h\0! ignored_file\0";
                var status = GitCommandHelpers.GetStatusChangedFilesFromString(module, statusString);
                Assert.IsTrue(status.Count == 2);
                Assert.IsTrue(status[0].Name == "adfs.h");
                Assert.IsTrue(status[1].Name == "ignored_file");
            }
            var testModule = new GitModule("C:\\Test\\SuperProject");
            var status = GitCommandHelpers.ParseSubmoduleStatus(text, testModule, fileName);
            Assert.AreEqual(ObjectId.Parse("b5a3d51777c85a9aeee534c382b5ccbb86b485d3"), status.Commit);
            Assert.AreEqual(fileName, status.Name);
            Assert.AreEqual(ObjectId.Parse("a17ea0c8ebe9d8cd7e634ba44559adffe633c11d"), status.OldCommit);
            Assert.AreEqual(fileName, status.OldName);
            status = GitCommandHelpers.ParseSubmoduleStatus(text, testModule, fileName);
            Assert.AreEqual(ObjectId.Parse("0cc457d030e92f804569407c7cd39893320f9740"), status.Commit);
            Assert.AreEqual(fileName, status.Name);
            Assert.AreEqual(ObjectId.Parse("2fb88514cfdc37a2708c24f71eca71c424b8d402"), status.OldCommit);
            Assert.AreEqual(fileName, status.OldName);
            status = GitCommandHelpers.ParseSubmoduleStatus(text, testModule, fileName);

            Assert.AreEqual(ObjectId.Parse("b5a3d51777c85a9aeee534c382b5ccbb86b485d3"), status.Commit);
            Assert.AreEqual(fileName, status.Name);
            Assert.AreEqual(ObjectId.Parse("a17ea0c8ebe9d8cd7e634ba44559adffe633c11d"), status.OldCommit);
            Assert.AreEqual("Externals/conemu-inside-a", status.OldName);

            text = "diff --git a/Externals/ICSharpCode.TextEditor b/Externals/ICSharpCode.TextEditor\r\nnew file mode 160000\r\nindex 000000000..05321769f\r\n--- /dev/null\r\n+++ b/Externals/ICSharpCode.TextEditor\r\n@@ -0,0 +1 @@\r\n+Subproject commit 05321769f039f39fa7f6748e8f30d5c8f157c7dc\r\n";
            fileName = "Externals/ICSharpCode.TextEditor";

            status = GitCommandHelpers.ParseSubmoduleStatus(text, testModule, fileName);

            Assert.AreEqual(ObjectId.Parse("05321769f039f39fa7f6748e8f30d5c8f157c7dc"), status.Commit);
            Assert.AreEqual(fileName, status.Name);
            Assert.IsNull(status.OldCommit);
            Assert.AreEqual("Externals/ICSharpCode.TextEditor", status.OldName);
        }

        [Test]
        public void SubmoduleSyncCmd()
        {
            Assert.AreEqual("submodule sync \"foo\"", GitCommandHelpers.SubmoduleSyncCmd("foo"));
            Assert.AreEqual("submodule sync", GitCommandHelpers.SubmoduleSyncCmd(""));
            Assert.AreEqual("submodule sync", GitCommandHelpers.SubmoduleSyncCmd(null));
        }

        [Test]
        public void AddSubmoduleCmd()
        {
            Assert.AreEqual(
                "submodule add -b \"branch\" \"remotepath\" \"localpath\"",
                GitCommandHelpers.AddSubmoduleCmd("remotepath", "localpath", "branch", force: false));

            Assert.AreEqual(
                "submodule add \"remotepath\" \"localpath\"",
                GitCommandHelpers.AddSubmoduleCmd("remotepath", "localpath", branch: null, force: false));

            Assert.AreEqual(
                "submodule add -f -b \"branch\" \"remotepath\" \"localpath\"",
                GitCommandHelpers.AddSubmoduleCmd("remotepath", "localpath", "branch", force: true));

            Assert.AreEqual(
                "submodule add -f -b \"branch\" \"remote/path\" \"local/path\"",
                GitCommandHelpers.AddSubmoduleCmd("remote\\path", "local\\path", "branch", force: true));
        }

        [Test]
        public void RevertCmd()
        {
            Assert.AreEqual(
                "revert commit",
                GitCommandHelpers.RevertCmd("commit", autoCommit: true, parentIndex: 0));

            Assert.AreEqual(
                "revert --no-commit commit",
                GitCommandHelpers.RevertCmd("commit", autoCommit: false, parentIndex: 0));

            Assert.AreEqual(
                "revert -m 1 commit",
                GitCommandHelpers.RevertCmd("commit", autoCommit: true, parentIndex: 1));
        }

        [Test]
        public void CloneCmd()
        {
            Assert.AreEqual(
                "clone -v --progress \"from\" \"to\"",
                GitCommandHelpers.CloneCmd("from", "to"));
            Assert.AreEqual(
                "clone -v --progress \"from/path\" \"to/path\"",
                GitCommandHelpers.CloneCmd("from\\path", "to\\path"));
            Assert.AreEqual(
                "clone -v --bare --progress \"from\" \"to\"",
                GitCommandHelpers.CloneCmd("from", "to", central: true));
            Assert.AreEqual(
                "clone -v --recurse-submodules --progress \"from\" \"to\"",
                GitCommandHelpers.CloneCmd("from", "to", initSubmodules: true));
            Assert.AreEqual(
                "clone -v --recurse-submodules --progress \"from\" \"to\"",
                GitCommandHelpers.CloneCmd("from", "to", initSubmodules: true));
            Assert.AreEqual(
                "clone -v --depth 2 --progress \"from\" \"to\"",
                GitCommandHelpers.CloneCmd("from", "to", depth: 2));
            Assert.AreEqual(
                "clone -v --single-branch --progress \"from\" \"to\"",
                GitCommandHelpers.CloneCmd("from", "to", isSingleBranch: true));
            Assert.AreEqual(
                "clone -v --no-single-branch --progress \"from\" \"to\"",
                GitCommandHelpers.CloneCmd("from", "to", isSingleBranch: false));
            Assert.AreEqual(
                "clone -v --progress --branch branch \"from\" \"to\"",
                GitCommandHelpers.CloneCmd("from", "to", branch: "branch"));
            Assert.AreEqual(
                "clone -v --progress --no-checkout \"from\" \"to\"",
                GitCommandHelpers.CloneCmd("from", "to", branch: null));
            Assert.AreEqual(
                "lfs clone -v --progress \"from\" \"to\"",
                GitCommandHelpers.CloneCmd("from", "to", lfs: true));
        }

        [Test]
        public void CheckoutCmd()
        {
            Assert.AreEqual(
                "checkout \"branch\"",
                GitCommandHelpers.CheckoutCmd("branch", LocalChangesAction.DontChange));
            Assert.AreEqual(
                "checkout --merge \"branch\"",
                GitCommandHelpers.CheckoutCmd("branch", LocalChangesAction.Merge));
            Assert.AreEqual(
                "checkout --force \"branch\"",
                GitCommandHelpers.CheckoutCmd("branch", LocalChangesAction.Reset));
            Assert.AreEqual(
                "checkout \"branch\"",
                GitCommandHelpers.CheckoutCmd("branch", LocalChangesAction.Stash));
        }

        [Test]
        public void RemoveCmd()
        {
            // TODO file names should be quoted

            Assert.AreEqual(
                "rm --force -r .",
                GitCommandHelpers.RemoveCmd());
            Assert.AreEqual(
                "rm -r .",
                GitCommandHelpers.RemoveCmd(force: false));
            Assert.AreEqual(
                "rm --force .",
                GitCommandHelpers.RemoveCmd(isRecursive: false));
            Assert.AreEqual(
                "rm --force -r a b c",
                GitCommandHelpers.RemoveCmd(files: new[] { "a", "b", "c" }));
        }

        [Test]
        public void BranchCmd()
        {
            // TODO split this into BranchCmd and CheckoutCmd

            Assert.AreEqual(
                "checkout -b \"branch\" \"revision\"",
                GitCommandHelpers.BranchCmd("branch", "revision", checkout: true));
            Assert.AreEqual(
                "branch \"branch\" \"revision\"",
                GitCommandHelpers.BranchCmd("branch", "revision", checkout: false));
            Assert.AreEqual(
                "checkout -b \"branch\"",
                GitCommandHelpers.BranchCmd("branch", null, checkout: true));
            Assert.AreEqual(
                "checkout -b \"branch\"",
                GitCommandHelpers.BranchCmd("branch", "", checkout: true));
            Assert.AreEqual(
                "checkout -b \"branch\"",
                GitCommandHelpers.BranchCmd("branch", "  ", checkout: true));
        }

        [Test]
        public void PushTagCmd()
        {
            // TODO test case where this is false
            Assert.True(GitCommandHelpers.VersionInUse.PushCanAskForProgress);

            Assert.AreEqual(
                "push --progress \"path\" tag tag",
                GitCommandHelpers.PushTagCmd("path", "tag", all: false));
            Assert.AreEqual(
                "push --progress \"path\" tag tag",
                GitCommandHelpers.PushTagCmd("path", " tag ", all: false));
            Assert.AreEqual(
                "push --progress \"path/path\" tag tag",
                GitCommandHelpers.PushTagCmd("path\\path", " tag ", all: false));
            Assert.AreEqual(
                "push --progress \"path\" --tags",
                GitCommandHelpers.PushTagCmd("path", "tag", all: true));
            Assert.AreEqual(
                "push -f --progress \"path\" --tags",
                GitCommandHelpers.PushTagCmd("path", "tag", all: true, force: ForcePushOptions.Force));
            Assert.AreEqual(
                "push --force-with-lease --progress \"path\" --tags",
                GitCommandHelpers.PushTagCmd("path", "tag", all: true, force: ForcePushOptions.ForceWithLease));

            // TODO this should probably throw rather than return an empty string
            Assert.AreEqual(
                "",
                GitCommandHelpers.PushTagCmd("path", "", all: false));
        }

        [Test]
        public void StashSaveCmd()
        {
            // TODO test case where message string contains quotes
            // TODO test case where message string contains newlines
            // TODO test case where selectedFiles contains whitespaces (not currently quoted)

            // TODO test case where this is false
            Assert.True(GitCommandHelpers.VersionInUse.StashUntrackedFilesSupported);

            Assert.AreEqual(
                "stash save",
                GitCommandHelpers.StashSaveCmd(untracked: false, keepIndex: false, null, Array.Empty<string>()));

            Assert.AreEqual(
                "stash save",
                GitCommandHelpers.StashSaveCmd(untracked: false, keepIndex: false, null, null));

            Assert.AreEqual(
                "stash save -u",
                GitCommandHelpers.StashSaveCmd(untracked: true, keepIndex: false, null, null));

            Assert.AreEqual(
                "stash save --keep-index",
                GitCommandHelpers.StashSaveCmd(untracked: false, keepIndex: true, null, null));
            Assert.AreEqual(
                "stash save --keep-index",
                GitCommandHelpers.StashSaveCmd(untracked: false, keepIndex: true, null, null));

            Assert.AreEqual(
                "stash save \"message\"",
                GitCommandHelpers.StashSaveCmd(untracked: false, keepIndex: false, "message", null));

            Assert.AreEqual(
                "stash push -- a b",
                GitCommandHelpers.StashSaveCmd(untracked: false, keepIndex: false, null, new[] { "a", "b" }));
        }

        [Test]
        public void ContinueBisectCmd()
        {
            Assert.AreEqual(
                "bisect good",
                GitCommandHelpers.ContinueBisectCmd(GitBisectOption.Good));
            Assert.AreEqual(
                "bisect bad",
                GitCommandHelpers.ContinueBisectCmd(GitBisectOption.Bad));
            Assert.AreEqual(
                "bisect skip",
                GitCommandHelpers.ContinueBisectCmd(GitBisectOption.Skip));
            Assert.AreEqual(
                "bisect good rev1 rev2",
                GitCommandHelpers.ContinueBisectCmd(GitBisectOption.Good, "rev1", "rev2"));
        }

        [Test]
        public void RebaseCmd()
        {
            Assert.AreEqual(
                "rebase \"branch\"",
                GitCommandHelpers.RebaseCmd("branch", interactive: false, preserveMerges: false, autosquash: false, autoStash: false));
            Assert.AreEqual(
                "rebase -i --no-autosquash \"branch\"",
                GitCommandHelpers.RebaseCmd("branch", interactive: true, preserveMerges: false, autosquash: false, autoStash: false));
            Assert.AreEqual(
                "rebase --preserve-merges \"branch\"",
                GitCommandHelpers.RebaseCmd("branch", interactive: false, preserveMerges: true, autosquash: false, autoStash: false));
            Assert.AreEqual(
                "rebase \"branch\"",
                GitCommandHelpers.RebaseCmd("branch", interactive: false, preserveMerges: false, autosquash: true, autoStash: false));
            Assert.AreEqual(
                "rebase --autostash \"branch\"",
                GitCommandHelpers.RebaseCmd("branch", interactive: false, preserveMerges: false, autosquash: false, autoStash: true));
            Assert.AreEqual(
                "rebase -i --autosquash \"branch\"",
                GitCommandHelpers.RebaseCmd("branch", interactive: true, preserveMerges: false, autosquash: true, autoStash: false));
            Assert.AreEqual(
                "rebase -i --autosquash --preserve-merges --autostash \"branch\"",
                GitCommandHelpers.RebaseCmd("branch", interactive: true, preserveMerges: true, autosquash: true, autoStash: true));

            // TODO quote 'onto'?

            Assert.AreEqual(
                "rebase \"from\" \"branch\" --onto onto",
                GitCommandHelpers.RebaseCmd("branch", interactive: false, preserveMerges: false, autosquash: false, autoStash: false, "from", "onto"));

            Assert.Throws<ArgumentException>(
                () => GitCommandHelpers.RebaseCmd("branch", false, false, false, false, from: null, onto: "onto"));

            Assert.Throws<ArgumentException>(
                () => GitCommandHelpers.RebaseCmd("branch", false, false, false, false, from: "from", onto: null));
        }

        [Test]
        public void CleanUpCmd()
        {
            Assert.AreEqual(
                "clean -f",
                GitCommandHelpers.CleanUpCmd(dryRun: false, directories: false, nonIgnored: true, ignored: false));
            Assert.AreEqual(
                "clean --dry-run",
                GitCommandHelpers.CleanUpCmd(dryRun: true, directories: false, nonIgnored: true, ignored: false));
            Assert.AreEqual(
                "clean -d -f",
                GitCommandHelpers.CleanUpCmd(dryRun: false, directories: true, nonIgnored: true, ignored: false));
            Assert.AreEqual(
                "clean -x -f",
                GitCommandHelpers.CleanUpCmd(dryRun: false, directories: false, nonIgnored: false, ignored: false));
            Assert.AreEqual(
                "clean -X -f",
                GitCommandHelpers.CleanUpCmd(dryRun: false, directories: false, nonIgnored: true, ignored: true));
            Assert.AreEqual(
                "clean -X -f",
                GitCommandHelpers.CleanUpCmd(dryRun: false, directories: false, nonIgnored: false, ignored: true));
            Assert.AreEqual(
                "clean -f paths",
                GitCommandHelpers.CleanUpCmd(dryRun: false, directories: false, nonIgnored: true, ignored: false, "paths"));
        }

        [Test]
        public void GetAllChangedFilesCmd()
        {
            Assert.AreEqual(
                "status --porcelain=2 -z --untracked-files --ignore-submodules",
                GitCommandHelpers.GetAllChangedFilesCmd(excludeIgnoredFiles: true, UntrackedFilesMode.Default, IgnoreSubmodulesMode.Default));
            Assert.AreEqual(
                "status --porcelain=2 -z --untracked-files --ignore-submodules --ignored",
                GitCommandHelpers.GetAllChangedFilesCmd(excludeIgnoredFiles: false, UntrackedFilesMode.Default, IgnoreSubmodulesMode.Default));
            Assert.AreEqual(
                "status --porcelain=2 -z --untracked-files=no --ignore-submodules",
                GitCommandHelpers.GetAllChangedFilesCmd(excludeIgnoredFiles: true, UntrackedFilesMode.No, IgnoreSubmodulesMode.Default));
            Assert.AreEqual(
                "status --porcelain=2 -z --untracked-files=normal --ignore-submodules",
                GitCommandHelpers.GetAllChangedFilesCmd(excludeIgnoredFiles: true, UntrackedFilesMode.Normal, IgnoreSubmodulesMode.Default));
            Assert.AreEqual(
                "status --porcelain=2 -z --untracked-files=all --ignore-submodules",
                GitCommandHelpers.GetAllChangedFilesCmd(excludeIgnoredFiles: true, UntrackedFilesMode.All, IgnoreSubmodulesMode.Default));
            Assert.AreEqual(
                "status --porcelain=2 -z --untracked-files --ignore-submodules=none",
                GitCommandHelpers.GetAllChangedFilesCmd(excludeIgnoredFiles: true, UntrackedFilesMode.Default, IgnoreSubmodulesMode.None));
            Assert.AreEqual(
                "status --porcelain=2 -z --untracked-files --ignore-submodules=none",
                GitCommandHelpers.GetAllChangedFilesCmd(excludeIgnoredFiles: true, UntrackedFilesMode.Default));
            Assert.AreEqual(
                "status --porcelain=2 -z --untracked-files --ignore-submodules=untracked",
                GitCommandHelpers.GetAllChangedFilesCmd(excludeIgnoredFiles: true, UntrackedFilesMode.Default, IgnoreSubmodulesMode.Untracked));
            Assert.AreEqual(
                "status --porcelain=2 -z --untracked-files --ignore-submodules=dirty",
                GitCommandHelpers.GetAllChangedFilesCmd(excludeIgnoredFiles: true, UntrackedFilesMode.Default, IgnoreSubmodulesMode.Dirty));
            Assert.AreEqual(
                "status --porcelain=2 -z --untracked-files --ignore-submodules=all",
                GitCommandHelpers.GetAllChangedFilesCmd(excludeIgnoredFiles: true, UntrackedFilesMode.Default, IgnoreSubmodulesMode.All));
            Assert.AreEqual(
                "--no-optional-locks status --porcelain=2 -z --untracked-files --ignore-submodules",
                GitCommandHelpers.GetAllChangedFilesCmd(excludeIgnoredFiles: true, UntrackedFilesMode.Default, IgnoreSubmodulesMode.Default, noLocks: true));
        }

        [Test]
        public void MergeBranchCmd()
        {
            Assert.AreEqual(
                "merge branch",
                GitCommandHelpers.MergeBranchCmd("branch", allowFastForward: true, squash: false, noCommit: false, allowUnrelatedHistories: false, message: null, log: null, strategy: null));
            Assert.AreEqual(
                "merge --no-ff branch",
                GitCommandHelpers.MergeBranchCmd("branch", allowFastForward: false, squash: false, noCommit: false, allowUnrelatedHistories: false, message: null, log: null, strategy: null));
            Assert.AreEqual(
                "merge --squash branch",
                GitCommandHelpers.MergeBranchCmd("branch", allowFastForward: true, squash: true, noCommit: false, allowUnrelatedHistories: false, message: null, log: null, strategy: null));
            Assert.AreEqual(
                "merge --no-commit branch",
                GitCommandHelpers.MergeBranchCmd("branch", allowFastForward: true, squash: false, noCommit: true, allowUnrelatedHistories: false, message: null, log: null, strategy: null));
            Assert.AreEqual(
                "merge --allow-unrelated-histories branch",
                GitCommandHelpers.MergeBranchCmd("branch", allowFastForward: true, squash: false, noCommit: false, allowUnrelatedHistories: true, message: null, log: null, strategy: null));
            Assert.AreEqual(
                "merge -m \"message\" branch",
                GitCommandHelpers.MergeBranchCmd("branch", allowFastForward: true, squash: false, noCommit: false, allowUnrelatedHistories: false, message: "message", log: null, strategy: null));
            Assert.AreEqual(
                "merge --log=0 branch",
                GitCommandHelpers.MergeBranchCmd("branch", allowFastForward: true, squash: false, noCommit: false, allowUnrelatedHistories: false, message: null, log: 0, strategy: null));
            Assert.AreEqual(
                "merge --strategy=strategy branch",
                GitCommandHelpers.MergeBranchCmd("branch", allowFastForward: true, squash: false, noCommit: false, allowUnrelatedHistories: false, message: null, log: null, strategy: "strategy"));
            Assert.AreEqual(
                "merge --no-ff --strategy=strategy --squash --no-commit --allow-unrelated-histories -m \"message\" --log=1 branch",
                GitCommandHelpers.MergeBranchCmd("branch", allowFastForward: false, squash: true, noCommit: true, allowUnrelatedHistories: true, message: "message", log: 1, strategy: "strategy"));
        }

        [Test]
        public void ApplyDiffPatchCmd()
        {
            Assert.AreEqual(
                "apply \"hello/world.patch\"",
                GitCommandHelpers.ApplyDiffPatchCmd(false, "hello\\world.patch"));
            Assert.AreEqual(
                "apply --ignore-whitespace \"hello/world.patch\"",
                GitCommandHelpers.ApplyDiffPatchCmd(true, "hello\\world.patch"));
        }

        [Test]
        public void ApplyMailboxPatchCmd()
        {
            Assert.AreEqual(
                "am --3way --signoff \"hello/world.patch\"",
                GitCommandHelpers.ApplyMailboxPatchCmd(false, "hello\\world.patch"));
            Assert.AreEqual(
                "am --3way --signoff --ignore-whitespace \"hello/world.patch\"",
                GitCommandHelpers.ApplyMailboxPatchCmd(true, "hello\\world.patch"));
            Assert.AreEqual(
                "am --3way --signoff --ignore-whitespace",
                GitCommandHelpers.ApplyMailboxPatchCmd(true));