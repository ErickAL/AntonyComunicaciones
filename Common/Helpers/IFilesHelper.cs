using System.IO;

namespace App.Common.Helpers
{
    public interface IFilesHelper
    {
        byte[] ReadFully(Stream input);

    }
}