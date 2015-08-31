using System.IO;
using System.Net.Sockets;
using System.Net;
using System;
using System.Windows;
using System.ComponentModel;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;


public class Comunicator : IDisposable
{
    Stream newStream;
    protected BinaryReader BinaryReader { get; private set; }
    protected BinaryWriter BinaryWriter { get; private set; }

    public Comunicator(Stream IOStream)
    {
        newStream = IOStream;
        if (newStream != null)
        {
            BinaryWriter = new BinaryWriter(newStream);
            BinaryReader = new BinaryReader(newStream);
        }
    }

    public bool IsStreamDescribed
    {
        get { return newStream != null; }
    }

    public string ReadText()
    {
        try
        {
            return BinaryReader.ReadString();
        }
        catch (IOException exception)
        {
            Console.WriteLine("Cannot write into stream: (Extendend info) \n" + exception.Message);
            return "!! NO CONNECTION !!";
        }
        catch (NullReferenceException exception )
        {
            MessageBox.Show("Reader object doesnt exist. Propably not Connected yet." + exception.Message);
            return "!! NO CONNECTION !!";
        }
    }

    public void SendText(string text)
    {
        try
        {
            BinaryWriter.Write(text);
        }
        catch (IOException exception)
        {
            MessageBox.Show("No connection, cannot write into stream: (Extended)\n" + exception.Message);
        }
        catch (NullReferenceException exception)
        {
            Console.WriteLine("Not Connected yet. " + exception);
        }
    }

    public void Dispose()
    {
        if (IsStreamDescribed)
        {
            newStream.Close();
            BinaryReader.Close();
            BinaryWriter.Close();
        }
        
    }
}
