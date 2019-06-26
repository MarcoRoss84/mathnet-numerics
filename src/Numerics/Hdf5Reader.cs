#if NET40 || NET461

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HDF.PInvoke;
using System.Runtime.InteropServices;

namespace MathNet.Numerics
{
    public class Hdf5Reader
    {
        private long fileId, dsId, spaceId, typeId;
        private readonly string filename;

        public Hdf5Reader(string filename)
        {
            this.filename = filename;
        }

        public bool readHdf5(string datasetPath, out float[] datasetOut)
        {
            datasetOut = null;
            var dataset = new List<float>();

            try
            {
                if (!loadHdf5Data(datasetPath, 4, out int size, out byte[] dataBytes))
                    return false;

                foreach (var slice in Batch(dataBytes, size))
                    dataset.Add(BitConverter.ToSingle(slice, 0));
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                closeH5();
            }

            datasetOut = dataset.ToArray();
            return true;
        }

        public bool readHdf5(string datasetPath, out double[] datasetOut)
        {
            datasetOut = null;
            var dataset = new List<double>();

            try
            {
                if(!loadHdf5Data(datasetPath, 8, out int size, out byte[] dataBytes))
                    return false;

                foreach (var slice in Batch(dataBytes, size))
                    dataset.Add(BitConverter.ToDouble(slice, 0));
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                closeH5();
            }

            datasetOut = dataset.ToArray();
            return true;
        }

        private void closeH5()
        {
            if (typeId != 0) H5T.close(typeId);
            if (spaceId != 0) H5S.close(spaceId);
            if (dsId != 0) H5D.close(dsId);
            if (fileId != 0) H5F.close(fileId);
            fileId = dsId = spaceId = typeId = 0;
        }

        private bool loadHdf5Data(string datasetPath, int expectedSize, out int size, out byte[] dataBytes)
        {
            fileId = H5F.open(filename, H5F.ACC_RDONLY);
            dsId = H5D.open(fileId, datasetPath);
            spaceId = H5D.get_space(dsId);
            typeId = H5D.get_type(dsId);
            var rank = H5S.get_simple_extent_ndims(spaceId);
            var dims = new ulong[rank];
            var maxDims = new ulong[rank];
            H5S.get_simple_extent_dims(spaceId, dims, maxDims);
            var sizeData = H5T.get_size(typeId);
            size = sizeData.ToInt32();
            if (size != expectedSize)
            {
                dataBytes = null;
                return false;
            }
            var bytearray_elements = dims.Aggregate((r, i) => r * i);
            dataBytes = new byte[bytearray_elements * (ulong)size];
            var pinnedArray = GCHandle.Alloc(dataBytes, GCHandleType.Pinned);
            H5D.read(dsId, typeId, H5S.ALL, H5S.ALL, H5P.DEFAULT, pinnedArray.AddrOfPinnedObject());
            pinnedArray.Free();
            return true;
        }

        private static IEnumerable<T[]> Batch<T>(IEnumerable<T> source, int size)
        {
            T[] bucket = null;
            var count = 0;

            foreach (var item in source)
            {
                if (bucket == null)
                    bucket = new T[size];

                bucket[count++] = item;

                if (count != size)
                    continue;

                yield return bucket;

                bucket = null;
                count = 0;
            }

            // Return the last bucket with all remaining elements
            //if (bucket != null && count > 0)
            //    yield return bucket.Take(count);
        }
    }
}

#endif
