// WARNING
// This file has been generated automatically by MonoDevelop to
// mirror C# types. Changes in this file made by drag-connecting
// from the UI designer will be synchronized back to C#, but
// more complex manual changes may not transfer correctly.


#import "vc_ManageVeggies.h"

@implementation vc_ManageVeggies

- (void)dealloc {
    [lblFeedback release];
    [btnNewVeggie release];
    [btnEditVeggie release];
    [btnDeleteVeggie release];
    [super dealloc];
}
- (void)viewDidUnload {
    [lblFeedback release];
    lblFeedback = nil;
    [btnNewVeggie release];
    btnNewVeggie = nil;
    [btnEditVeggie release];
    btnEditVeggie = nil;
    [btnDeleteVeggie release];
    btnDeleteVeggie = nil;
    [super viewDidUnload];
}
@end
