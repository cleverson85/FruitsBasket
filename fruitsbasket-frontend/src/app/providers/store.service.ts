import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ApiRoute } from '../shared/enum/apiRoutes.enum';
import BaseService from './common/base.service';

@Injectable({
  providedIn: 'root',
})
export class StoreService extends BaseService {
  salvar(stores: any) {
    this.httpClient.post(this.API + ApiRoute.STORE, stores).subscribe(
      (result: any) => {
        this.cartService.setMessage({});
        this.toasterService.showToastSuccess('Operação efetuada com sucesso.');
        this.router.navigate(['home']);
      },
      (err: HttpErrorResponse) => {
        const { error } = err;
        console.log('ERROR => ' + error);
        this.toasterService.showToastError(
          'Não foi possível exetuar a operação, tente novamente mais tarde.',
        );
      },
    );
  }
}