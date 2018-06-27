using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using powerGateServer.SDK;

namespace fusionLifecycle
{
    [DataServiceKey("WorkspaceId", "DmsId","FileId")]
    [DataServiceEntity]
    public class FusionLifecycleItemAttachment
    {
        public long WorkspaceId { get; set; }
        public long DmsId { get; set; }
        public long FileId { get; set; }
        public long Version { get; set; }
        public long FolderId { get; set; }
        public string Owner { get; set; }
        public string Uri { get; set; }
        public string FileName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Revision { get; set; }
    }

    

    public class FusionLifecycleItemAttachments : ServiceMethod<FusionLifecycleItemAttachment>
    {
        public override string Name
        {
            get { return "FlItemAttachments"; }
        }

        public override IEnumerable<FusionLifecycleItemAttachment> Query(IExpression<FusionLifecycleItemAttachment> expression)
        {
            List<FusionLifecycleItemAttachment> attachments = new List<FusionLifecycleItemAttachment>();
            var wsId = expression.Where.FirstOrDefault(w => w.PropertyName.Equals("WorkspaceId"));
            var iId = expression.Where.FirstOrDefault(w => w.PropertyName.Equals("Id"));
            if (wsId != null && iId != null)
            {
                long workspaceId = long.Parse(wsId.Value.ToString());
                long itemId = long.Parse(iId.Value.ToString());
                var flAttachments = FlHelper.instance.GetAttachments(workspaceId, itemId);
                foreach (var file in flAttachments.List.Data)
                    attachments.Add(new FusionLifecycleItemAttachment() { Description = file.File.Description, DmsId = file.File.DmsId, FileId = file.File.FileId, FileName = file.File.FileName, FolderId = file.File.FolderId, Owner = file.File.CreatedDisplayName, Revision = file.File.FileVersion.ToString(), Status = file.File.FileStatus.Status, TimeStamp = file.File.TimeStamp, Uri = file.Uri, Version = file.File.VersionId, WorkspaceId = file.File.WorkspaceId });
            }
            return attachments;
        }

        public override void Update(FusionLifecycleItemAttachment entity)
        {
            
        }

        public override void Create(FusionLifecycleItemAttachment entity)
        {

        }

        public override void Delete(FusionLifecycleItemAttachment entity)
        {
        }
    }

    [DataServiceKey("WorkspaceId","Id")]
    [DataServiceEntity]
    public class FusionLifecycleFile : Streamable
    {
        public long Id { get; set; }
        public long WorkspaceId { get; set; }
        public string Description { get; set; }

        public FusionLifecycleFile()
        {
            /**
             * Optional: setting a FileName
             * PowerGateServer adds "Content-Disposition' to response header with value: filename=value
             * e.g. The Chrome browser uses this Name if we later save the PDF in our browser
             */
            //FileName = "TestFile.pdf";
        }

        public override string GetContentType()
        {
            return ContentTypes.Application.Pdf;
        }
    }

    public class FusionLifecycleFileService : ServiceMethod<FusionLifecycleFile>, IStreamableServiceMethod<FusionLifecycleFile>
    {
        public override string Name
        {
            get { return "FlFile"; }
        }

        public IStream Download(FusionLifecycleFile entity)
        {
            return null;
        }

        public void Upload(FusionLifecycleFile entity, IStream stream)
        {
            FlHelper.instance.AddAttachment(entity.WorkspaceId, entity.Id, entity.FileName, entity.Description, stream.Source);
        }

        public void DeleteStream(FusionLifecycleFile entity)
        {
        }

        public override IEnumerable<FusionLifecycleFile> Query(IExpression<FusionLifecycleFile> expression)
        {
            List<FusionLifecycleFile> mediaDocs = new List<FusionLifecycleFile>();
            return mediaDocs;
        }

        public override void Update(FusionLifecycleFile entity)
        {
        }

        public override void Create(FusionLifecycleFile entity)
        {
        }

        public override void Delete(FusionLifecycleFile entity)
        {
        }
    }


}
