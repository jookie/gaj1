//
//  AppDelegate+Notification.h
//
// Created by Olivier Louvignes on 2012-05-06.
//
// Copyright 2012 Olivier Louvignes. All rights reserved.
// MIT Licensed

#import "ApplicationDelegate.h"

@interface ApplicationDelegate (Notification)

- (void)application:(UIApplication *)application didRegisterForRemoteNotificationsWithDeviceToken:(NSData *)deviceToken;
- (void)application:(UIApplication *)application didFailToRegisterForRemoteNotificationsWithError:(NSError *)error;
- (void)application:(UIApplication *)application didReceiveRemoteNotification:(NSDictionary *)userInfo;

@end
