AutoScaler
==========

Tool to monitor an SQS queue and as needed create new servers based on an EC2 AMI image to handle the load. The configuration allows you to specify how many messages a server can handle in an hour, auto scaler will increase your server count as needed and shut the servers down when finished.

