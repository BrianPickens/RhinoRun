//
//  Plugin.m
//  iCloudKeyValue
//
//  Created by Gennadii Potapov on 30/7/16.
//  Copyright © 2016 General Arcade. All rights reserved.
//

#import <Foundation/Foundation.h>

void iCloudKV_Synchronize() {
    [[NSUbiquitousKeyValueStore defaultStore] synchronize];
}

void iCloudKV_SetInt(char * key, int value) {
    [[NSUbiquitousKeyValueStore defaultStore] setObject:[NSNumber numberWithInt:value] forKey:[NSString stringWithUTF8String:key]];

}

void iCloudKV_SetFloat(char * key, float value) {
    [[NSUbiquitousKeyValueStore defaultStore] setObject:[NSNumber numberWithFloat:value] forKey:[NSString stringWithUTF8String:key]];
}

void iCloudKV_SetString(char * key, char * value)
{
	[[NSUbiquitousKeyValueStore defaultStore] setObject:[NSString stringWithUTF8String:value] forKey: [NSString stringWithUTF8String:key]];
}

char* ConvertNSStringToCharArray (const char * string)
{
	if(string == NULL)
	{
		return NULL;
	}
	
	char* stringCopy = malloc(strlen(string) + 1);
	strcpy(stringCopy, string);
	return stringCopy;
}

int iCloudKV_GetInt(char * key) {
    NSNumber * num = (NSNumber *)([[NSUbiquitousKeyValueStore defaultStore] objectForKey:[NSString stringWithUTF8String:key]]);
    int i = 0;
    if (num != nil)
        i = [num intValue];
    return i;
}

float iCloudKV_GetFloat(char * key) {
    NSNumber * num = (NSNumber *)([[NSUbiquitousKeyValueStore defaultStore] objectForKey:[NSString stringWithUTF8String:key]]);
    float i = 0;
    if (num != nil)
        i = [num floatValue];
    return i;
}

char* iCloudKV_GetString(char * key) {
	NSString * newString = (NSString *)([[NSUbiquitousKeyValueStore defaultStore] objectForKey:[NSString stringWithUTF8String:key]]);
	return ConvertNSStringToCharArray([newString UTF8String]);
}

void iCloudKV_Reset() {
    NSUbiquitousKeyValueStore *kvStore = [NSUbiquitousKeyValueStore defaultStore];
    NSDictionary *kvd = [kvStore dictionaryRepresentation];
    NSArray *arr = [kvd allKeys];
    for (int i=0; i < arr.count; i++){
        NSString *key = [arr objectAtIndex:i];
        [kvStore removeObjectForKey:key];
    }
    [[NSUbiquitousKeyValueStore defaultStore] synchronize];
}
