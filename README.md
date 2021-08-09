# azure.devops.notify.dingtalk.robots

创建任务项
```json
{
    "subscriptionId": "bea5b36f-976e-40e5-a7e4-6b72ceb1dff0",
    "notificationId": 1,
    "id": "86a00bfb-28a6-4e5e-bed7-6069791f1996",
    "eventType": "workitem.updated",
    "publisherId": "tfs",
    "message": {
        "text": "Bug #13 (创建一个bug) transitioned to Approved by 欧俊\r\n(https://dev.azure.com/ouno/web/wi.aspx?pcguid=2245531e-4735-4f13-81d1-a4a33d5532a1&id=13)",
        "html": "<a href=\"https://dev.azure.com/ouno/web/wi.aspx?pcguid=2245531e-4735-4f13-81d1-a4a33d5532a1&amp;id=13\">Bug #13</a> (创建一个bug) transitioned to Approved by 欧俊",
        "markdown": "[Bug #13](https://dev.azure.com/ouno/web/wi.aspx?pcguid=2245531e-4735-4f13-81d1-a4a33d5532a1&id=13) (创建一个bug) transitioned to Approved by 欧俊"
    },
    "detailedMessage": {
        "text": "Bug #13 (创建一个bug) transitioned to Approved by 欧俊\r\n(https://dev.azure.com/ouno/web/wi.aspx?pcguid=2245531e-4735-4f13-81d1-a4a33d5532a1&id=13)\r\n\r\n- New State: Approved\r\n",
        "html": "<a href=\"https://dev.azure.com/ouno/web/wi.aspx?pcguid=2245531e-4735-4f13-81d1-a4a33d5532a1&amp;id=13\">Bug #13</a> (创建一个bug) transitioned to Approved by 欧俊<ul>\r\n<li>New State: Approved</li></ul>",
        "markdown": "[Bug #13](https://dev.azure.com/ouno/web/wi.aspx?pcguid=2245531e-4735-4f13-81d1-a4a33d5532a1&id=13) (创建一个bug) transitioned to Approved by 欧俊\r\n\r\n* New State: Approved\r\n"
    },
    "resource": {
        "id": 2,
        "workItemId": 13,
        "rev": 2,
        "revisedBy": {
            "id": "49b79148-4735-4daa-8acb-8406eaa643b1",
            "name": "欧俊 <luacloud@gmail.com>",
            "displayName": "欧俊",
            "url": "https://spsprodeus27.vssps.visualstudio.com/A0da6820a-0c82-40bd-8b31-bc1bb2a21a35/_apis/Identities/49b79148-4735-4daa-8acb-8406eaa643b1",
            "_links": {
                "avatar": {
                    "href": "https://dev.azure.com/ouno/_apis/GraphProfile/MemberAvatars/msa.MmZiNjhlMTAtZTBlMS03YTYwLTljNjMtNDBmNGY3M2MzN2Zi"
                }
            },
            "uniqueName": "luacloud@gmail.com",
            "imageUrl": "https://dev.azure.com/ouno/_apis/GraphProfile/MemberAvatars/msa.MmZiNjhlMTAtZTBlMS03YTYwLTljNjMtNDBmNGY3M2MzN2Zi",
            "descriptor": "msa.MmZiNjhlMTAtZTBlMS03YTYwLTljNjMtNDBmNGY3M2MzN2Zi"
        },
        "revisedDate": "9999-01-01T00:00:00Z",
        "fields": {
            "System.Rev": {
                "oldValue": 1,
                "newValue": 2
            },
            "System.AuthorizedDate": {
                "oldValue": "2021-08-07T15:41:43.08Z",
                "newValue": "2021-08-07T15:41:50.23Z"
            },
            "System.RevisedDate": {
                "oldValue": "2021-08-07T15:41:50.23Z",
                "newValue": "9999-01-01T00:00:00Z"
            },
            "System.State": {
                "oldValue": "New",
                "newValue": "Approved"
            },
            "System.Reason": {
                "oldValue": "New defect reported",
                "newValue": "Approved by the Product Owner"
            },
            "System.ChangedDate": {
                "oldValue": "2021-08-07T15:41:43.08Z",
                "newValue": "2021-08-07T15:41:50.23Z"
            },
            "System.Watermark": {
                "oldValue": 13,
                "newValue": 14
            },
            "Microsoft.VSTS.Common.StateChangeDate": {
                "oldValue": "2021-08-07T15:41:43.08Z",
                "newValue": "2021-08-07T15:41:50.23Z"
            }
        },
        "_links": {
            "self": {
                "href": "https://dev.azure.com/ouno/66776c68-10ec-4679-9652-d001a718d37f/_apis/wit/workItems/13/updates/2"
            },
            "workItemUpdates": {
                "href": "https://dev.azure.com/ouno/66776c68-10ec-4679-9652-d001a718d37f/_apis/wit/workItems/13/updates"
            },
            "parent": {
                "href": "https://dev.azure.com/ouno/66776c68-10ec-4679-9652-d001a718d37f/_apis/wit/workItems/13"
            },
            "html": {
                "href": "https://dev.azure.com/ouno/web/wi.aspx?pcguid=2245531e-4735-4f13-81d1-a4a33d5532a1&id=13"
            }
        },
        "url": "https://dev.azure.com/ouno/66776c68-10ec-4679-9652-d001a718d37f/_apis/wit/workItems/13/updates/2",
        "revision": {
            "id": 13,
            "rev": 2,
            "fields": {
                "System.AreaPath": "Virtual.Currency",
                "System.TeamProject": "Virtual.Currency",
                "System.IterationPath": "Virtual.Currency",
                "System.WorkItemType": "Bug",
                "System.State": "Approved",
                "System.Reason": "Approved by the Product Owner",
                "System.AssignedTo": "oujun@housecool.com <oujun@housecool.com>",
                "System.CreatedDate": "2021-08-07T15:41:43.08Z",
                "System.CreatedBy": "欧俊 <luacloud@gmail.com>",
                "System.ChangedDate": "2021-08-07T15:41:50.23Z",
                "System.ChangedBy": "欧俊 <luacloud@gmail.com>",
                "System.CommentCount": 0,
                "System.Title": "创建一个bug",
                "Microsoft.VSTS.Common.Severity": "1 - Critical",
                "Microsoft.VSTS.Common.StateChangeDate": "2021-08-07T15:41:50.23Z",
                "Microsoft.VSTS.Common.Priority": 1,
                "Microsoft.VSTS.Common.ValueArea": "Business",
                "Microsoft.VSTS.TCM.SystemInfo": "<div>系统信息</div>",
                "Microsoft.VSTS.TCM.ReproSteps": "步骤",
                "Microsoft.VSTS.Common.AcceptanceCriteria": "<div>这个是啥</div>"
            },
            "_links": {
                "self": {
                    "href": "https://dev.azure.com/ouno/66776c68-10ec-4679-9652-d001a718d37f/_apis/wit/workItems/13/revisions/2"
                },
                "workItemRevisions": {
                    "href": "https://dev.azure.com/ouno/66776c68-10ec-4679-9652-d001a718d37f/_apis/wit/workItems/13/revisions"
                },
                "parent": {
                    "href": "https://dev.azure.com/ouno/66776c68-10ec-4679-9652-d001a718d37f/_apis/wit/workItems/13"
                }
            },
            "url": "https://dev.azure.com/ouno/66776c68-10ec-4679-9652-d001a718d37f/_apis/wit/workItems/13/revisions/2"
        }
    },
    "resourceVersion": "1.0",
    "resourceContainers": {
        "collection": {
            "id": "2245531e-4735-4f13-81d1-a4a33d5532a1",
            "baseUrl": "https://dev.azure.com/ouno/"
        },
        "account": {
            "id": "0da6820a-0c82-40bd-8b31-bc1bb2a21a35",
            "baseUrl": "https://dev.azure.com/ouno/"
        },
        "project": {
            "id": "66776c68-10ec-4679-9652-d001a718d37f",
            "baseUrl": "https://dev.azure.com/ouno/"
        }
    },
    "createdDate": "2021-08-07T15:41:56.5943909Z"
}
```

添加任务项评论
```
{
  "subscriptionId": "ea7bd3d2-94e8-4dd7-9703-d7524beea45d",
  "notificationId": 1,
  "id": "4c19e2ca-d27c-4ed4-a69f-37ab2f2a95c7",
  "eventType": "workitem.commented",
  "publisherId": "tfs",
  "message": {
    "text": "Bug #13 (创建一个bug) commented on by 欧俊\r\n(https://dev.azure.com/ouno/web/wi.aspx?pcguid=2245531e-4735-4f13-81d1-a4a33d5532a1&id=13)",
    "html": "<a href=\"https://dev.azure.com/ouno/web/wi.aspx?pcguid=2245531e-4735-4f13-81d1-a4a33d5532a1&amp;id=13\">Bug #13</a> (创建一个bug) commented on by 欧俊",
    "markdown": "[Bug #13](https://dev.azure.com/ouno/web/wi.aspx?pcguid=2245531e-4735-4f13-81d1-a4a33d5532a1&id=13) (创建一个bug) commented on by 欧俊"
  },
  "detailedMessage": {
    "text": "Bug #13 (创建一个bug) commented on by 欧俊\r\n(https://dev.azure.com/ouno/web/wi.aspx?pcguid=2245531e-4735-4f13-81d1-a4a33d5532a1&id=13)\r\n\r\n评论\r\n",
    "html": "<a href=\"https://dev.azure.com/ouno/web/wi.aspx?pcguid=2245531e-4735-4f13-81d1-a4a33d5532a1&amp;id=13\">Bug #13</a> (创建一个bug) commented on by 欧俊<br/><div>评论</div>",
    "markdown": "[Bug #13](https://dev.azure.com/ouno/web/wi.aspx?pcguid=2245531e-4735-4f13-81d1-a4a33d5532a1&id=13) (创建一个bug) commented on by 欧俊\r\n\r\n评论\r\n"
  },
  "resource": {
    "id": 13,
    "rev": 3,
    "fields": {
      "System.AreaPath": "Virtual.Currency",
      "System.TeamProject": "Virtual.Currency",
      "System.IterationPath": "Virtual.Currency",
      "System.WorkItemType": "Bug",
      "System.State": "Approved",
      "System.Reason": "Approved by the Product Owner",
      "System.AssignedTo": "oujun@housecool.com <oujun@housecool.com>",
      "System.CreatedDate": "2021-08-07T15:41:43.08Z",
      "System.CreatedBy": "欧俊 <luacloud@gmail.com>",
      "System.ChangedDate": "2021-08-07T15:42:07.777Z",
      "System.ChangedBy": "欧俊 <luacloud@gmail.com>",
      "System.CommentCount": 1,
      "System.Title": "创建一个bug",
      "Microsoft.VSTS.Common.Severity": "1 - Critical",
      "Microsoft.VSTS.Common.StateChangeDate": "2021-08-07T15:41:50.23Z",
      "Microsoft.VSTS.Common.Priority": 1,
      "Microsoft.VSTS.Common.ValueArea": "Business",
      "System.History": "<div>评论</div>",
      "Microsoft.VSTS.TCM.SystemInfo": "<div>系统信息</div>",
      "Microsoft.VSTS.TCM.ReproSteps": "步骤",
      "Microsoft.VSTS.Common.AcceptanceCriteria": "<div>这个是啥</div>"
    },
    "commentVersionRef": {
      "commentId": 2743814,
      "version": 1,
      "url": "https://dev.azure.com/ouno/66776c68-10ec-4679-9652-d001a718d37f/_apis/wit/workItems/13/comments/2743814/versions/1"
    },
    "_links": {
      "self": {
        "href": "https://dev.azure.com/ouno/66776c68-10ec-4679-9652-d001a718d37f/_apis/wit/workItems/13"
      },
      "workItemUpdates": {
        "href": "https://dev.azure.com/ouno/66776c68-10ec-4679-9652-d001a718d37f/_apis/wit/workItems/13/updates"
      },
      "workItemRevisions": {
        "href": "https://dev.azure.com/ouno/66776c68-10ec-4679-9652-d001a718d37f/_apis/wit/workItems/13/revisions"
      },
      "workItemComments": {
        "href": "https://dev.azure.com/ouno/66776c68-10ec-4679-9652-d001a718d37f/_apis/wit/workItems/13/comments"
      },
      "html": {
        "href": "https://dev.azure.com/ouno/66776c68-10ec-4679-9652-d001a718d37f/_workitems/edit/13"
      },
      "workItemType": {
        "href": "https://dev.azure.com/ouno/66776c68-10ec-4679-9652-d001a718d37f/_apis/wit/workItemTypes/Bug"
      },
      "fields": {
        "href": "https://dev.azure.com/ouno/66776c68-10ec-4679-9652-d001a718d37f/_apis/wit/fields"
      }
    },
    "url": "https://dev.azure.com/ouno/66776c68-10ec-4679-9652-d001a718d37f/_apis/wit/workItems/13"
  },
  "resourceVersion": "1.0",
  "resourceContainers": {
    "collection": {
      "id": "2245531e-4735-4f13-81d1-a4a33d5532a1",
      "baseUrl": "https://dev.azure.com/ouno/"
    },
    "account": {
      "id": "0da6820a-0c82-40bd-8b31-bc1bb2a21a35",
      "baseUrl": "https://dev.azure.com/ouno/"
    },
    "project": {
      "id": "66776c68-10ec-4679-9652-d001a718d37f",
      "baseUrl": "https://dev.azure.com/ouno/"
    }
  },
  "createdDate": "2021-08-07T15:42:14.8870739Z"
}
```

