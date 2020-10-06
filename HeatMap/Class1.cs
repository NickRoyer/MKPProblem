using System;

namespace HeatMap
{
    public class HeatMapProcessor
    {
        
        
        /*
        Protected Sub performAllocationLoop()
        Dim ord As Order
        Dim rsc As Resource
        Dim ait As AllocatedItem

        While(ResourceManager.HasResources AndAlso OrderManager.HasOrders)

            ResourceManager.preAllocation()
            OrderManager.preAllocation()

            'Need decision on whether first pass should include bonus spots.
            'right now bonus spots are being scheduled.
            ord = OrderManager.nextOrderToAllocate
            If Not ord Is Nothing Then
                rsc = ResourceManager.nextResourceToAllocate(ord)
                If Not rsc Is Nothing Then
                    ait = Me.allocateItemToResource(rsc, ord)

                    If ait Is Nothing Then
                        'This means no item was able to be allocated from this order to this resource.
                        'So to prevent this combination from occuring again we remove the scenario as a possibility.
                        ord.removeResource(rsc)
                        rsc.removeOrder(ord)
                    End If
                Else
                    'This means that the order got chosen to be scheduled however there are no avails left that the contract applies to so force the order allocation
                    If ord.RemainingResourceList.Count > 0 Then
                        Me.orderAllocated(ord)
                    End If
                End If

                'The contract has scheduled all of its spots.
                If Not ord.RemainingItemList.hasItems() Then
                    Me.orderAllocated(ord)
                    'Else if there are no more resoures available for this order.
                ElseIf Not ord.RemainingResourceList.hasResources(ord) Then
                    Me.orderAllocated(ord)
                End If
            Else
                Exit While
            End If

        End While

    End Sub
    */
    }
}
