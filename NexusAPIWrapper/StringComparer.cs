using System;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;

public class StringComparer
{
    public string GetDifferences(string oldText, string newText)
    {
        var diffBuilder = new InlineDiffBuilder(new Differ());
        var diff = diffBuilder.BuildDiffModel(oldText, newText);

        return DiffToString(diff);
    }

    private string DiffToString(DiffPaneModel diff)
    {
        var result = new System.Text.StringBuilder();

        foreach (var line in diff.Lines)
        {
            switch (line.Type)
            {
                case ChangeType.Inserted:
                    result.AppendLine($"Added: {line.Text}");
                    break;
                case ChangeType.Deleted:
                    result.AppendLine($"Removed: {line.Text}");
                    break;
                case ChangeType.Modified:
                    result.AppendLine($"Modified: {line.Text}");
                    break;
                case ChangeType.Unchanged:
                    result.AppendLine($"Unchanged: {line.Text}");
                    break;
            }
        }

        return result.ToString();
    }
}


