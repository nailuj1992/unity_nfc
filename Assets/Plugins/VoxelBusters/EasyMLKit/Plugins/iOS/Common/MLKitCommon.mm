//
//  MLKitCommon.h
//  NativeMLKitiOS
//
//  Created by Ayyappa J on 27/05/22.
//

#import "MLKitCommon.h"

MLKitRect getRect(CGRect source)
{
    MLKitRect rect;
    rect.x = source.origin.x;
    rect.y = source.origin.y;
    rect.width = source.size.width;
    rect.height = source.size.height;
    return rect;
}

