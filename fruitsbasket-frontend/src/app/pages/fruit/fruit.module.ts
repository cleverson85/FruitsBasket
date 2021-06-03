import { NgModule } from '@angular/core';
import { MenuListModule } from 'src/app/components/menu-list/menu-list.module';
import { PaginationModule } from 'src/app/components/pagination/pagination.module';
import { SharedModule } from '../../shared/shared.module';
import { FruitEditComponent } from './fruit-edit/fruit-edit.component';
import { FruitComponent } from './fruit-list/fruit.component';
import { FruitRoutingModule } from './fruit-routing.module';

@NgModule({
  imports: [SharedModule, FruitRoutingModule, PaginationModule, MenuListModule],
  declarations: [FruitComponent, FruitEditComponent],
})
export class FruitModule {}
