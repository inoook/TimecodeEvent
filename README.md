# TimecodeEvent

_Externals/
- Video/
- appSetting.json
- settingA.json

appSetting.json
```json
{
    "osc": {
        "ip": "127.0.0.1",
        "port": 8010
    },
    "contents": [
        {
            "videoFile": "Video/VideoA.mp4",
            "oscJson": "settingA.json"
        },
        {
            "videoFile": "Video/VideoB.mp4",
            "oscJson": "settingB.json"
        }
    ]
}
```

settingA.json
```json
{
    "timecodes": [
        {
            "timecode": "0:00:00",
            "action": {
                "param": "000"
            }
        },
        {
            "timecode": "0:00:02",
            "action": {
                "param": "001"
            }
        },
        {
            "timecode": "0:00:04",
            "action": {
                "param": "002"
            }
        }
    ]
}
```
