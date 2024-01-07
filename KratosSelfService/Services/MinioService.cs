using System.Collections.ObjectModel;
using Minio;
using Minio.DataModel;
using Minio.DataModel.Args;
using Minio.Exceptions;

namespace KratosSelfService.Services;

public class MinioService(EnvService env)
{
    private readonly IMinioClient _minio = new MinioClient()
        .WithEndpoint(env.MinioEndpoint)
        .WithCredentials(env.MinioAccessKey, env.MinioSecretKey)
        .WithSSL()
        .WithRegion(env.MinioRegion)
        .Build();

    public async Task<Collection<Bucket>> GetBuckets()
    {
        var bucketsList = await _minio.ListBucketsAsync().ConfigureAwait(false);
        return bucketsList.Buckets;
    }

    public async Task<bool> BucketExists(string bucket)
    {
        var args = new BucketExistsArgs().WithBucket(bucket);
        return await _minio.BucketExistsAsync(args).ConfigureAwait(false);
    }

    public async Task DeleteObject(string bucket, string objectName)
    {
        var args = new RemoveObjectArgs().WithBucket(bucket).WithObject(objectName);
        await _minio.RemoveObjectAsync(args).ConfigureAwait(false);
    }

    public IObservable<Item> GetBucketObjectStats(string bucket)
    {
        var args = new ListObjectsArgs().WithBucket(bucket);
        return _minio.ListObjectsAsync(args);
    }

    public async Task<ObjectStat?> GetBucketObjectStat(string bucket, string objectName)
    {
        try
        {
            var args = new GetObjectArgs()
                .WithBucket(bucket)
                .WithObject(objectName);
            return await _minio.GetObjectAsync(args);
        }
        catch (ObjectNotFoundException)
        {
            return null;
        }
    }

    public async Task<byte[]?> GetBucketObject(string bucket, string objectName)
    {
        try
        {
            var memoryStream = new MemoryStream();
            var args = new GetObjectArgs()
                .WithBucket(bucket)
                .WithObject(objectName)
                .WithCallbackStream(stream => stream.CopyTo(memoryStream));
            await _minio.GetObjectAsync(args);
            var bytes = memoryStream.ToArray();
            await memoryStream.DisposeAsync();
            return bytes;
        }
        catch (ObjectNotFoundException)
        {
            return null;
        }
    }

    public async Task DeleteObjects(string bucket, IList<string> objectNames)
    {
        if (objectNames.Count == 0) return;
        var args = new RemoveObjectsArgs()
            .WithBucket(bucket)
            .WithObjects(objectNames);
        await _minio.RemoveObjectsAsync(args).ConfigureAwait(false);
    }

    public async Task AddObject(string bucket, string objectName, string contentType, MemoryStream stream)
    {
        var args = new PutObjectArgs()
            .WithBucket(bucket)
            .WithObject(objectName)
            .WithStreamData(stream)
            .WithObjectSize(stream.Length)
            .WithContentType(contentType);
        await _minio.PutObjectAsync(args).ConfigureAwait(false);
    }

    public async Task<bool> ObjectExists(string bucket, string objectName)
    {
        return await GetBucketObjectStat(bucket, objectName) != null;
    }
}