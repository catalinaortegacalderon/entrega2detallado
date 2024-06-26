﻿namespace Fire_Emblem_View;

internal abstract class AbstractView
{
    private readonly Script _script = new();

    public void WriteLine(object text)
    {
        Write($"{text}\n");
    }

    protected virtual void Write(object text)
    {
        _script.AddToScript(text.ToString());
    }

    public string ReadLine()
    {
        var nextInput = GetNextInput();
        _script.AddInput(nextInput);
        return nextInput;
    }

    protected abstract string GetNextInput();

    public void ExportScript(string path)
    {
        _script.ExportScript(path);
    }

    public string[] GetScript()
    {
        return _script.GetScript().Split('\n');
    }
}