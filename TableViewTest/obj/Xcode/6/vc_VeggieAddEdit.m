// WARNING
// This file has been generated automatically by MonoDevelop to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import "vc_VeggieAddEdit.h"

@implementation vc_VeggieAddEdit

- (void)dealloc {
    [txtVeggieName release];
    [boolYummy release];
    [txtDescription release];
    [btnSave release];
    [lblHeader release];
    [super dealloc];
}
- (void)viewDidUnload {
    [txtVeggieName release];
    txtVeggieName = nil;
    [boolYummy release];
    boolYummy = nil;
    [txtDescription release];
    txtDescription = nil;
    [btnSave release];
    btnSave = nil;
    [lblHeader release];
    lblHeader = nil;
    [super viewDidUnload];
}
@end
