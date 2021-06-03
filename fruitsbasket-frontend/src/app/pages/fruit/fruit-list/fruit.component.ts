import { Component, ElementRef, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { Subscription } from 'rxjs';
import { ToasterService } from 'src/app/providers/common/toaster.service';
import { FruitService } from 'src/app/providers/fruit.service';
import { ModalService } from 'src/app/providers/modal.service';
import { ApiRoute } from 'src/app/shared/enum/apiRoutes.enum';
import { Fruit } from '../../../models/fruit';

@Component({
  selector: 'app-fruit',
  templateUrl: './fruit.component.html',
  styleUrls: ['./fruit.component.scss'],
})
export class FruitComponent implements OnInit, OnDestroy {
  @ViewChild('valueToSearch') valueToSearch: ElementRef;

  subscription = new Subscription();
  fruits: Fruit[];
  pages: number;
  routeApi = ApiRoute.DELETE;

  constructor(
    private toasterService: ToasterService,
    private fruitService: FruitService,
    private modalService: ModalService,
  ) { }

  ngOnInit() {
    this.subscription.add(
      this.fruitService.getFruits(1).subscribe((result: any) => {
        this.configureItens(result);
      }
    ));
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  find(page?: number) {
    this.subscription.add(
      this.fruitService.getFruitByName(this.valueToSearch.nativeElement.value, page)
        .subscribe((result: any) => {
          if (result?.items.length === 0) {
            this.toasterService.showToastWarning('Nenhum item foi encontrado.');
            return;
          }

          this.configureItens(result);
        })
    );
  }

  configureItens(result: any) {
    if (result) {
      const { count, items } = result;
      this.fruits = items;
      this.pages = count;
    }
  }

  onPageChange(page: any) {
    this.find(page);
  }

  reloadList(event: any) {
    const { value } = event;
    this.fruits = value.items;
  }
}
