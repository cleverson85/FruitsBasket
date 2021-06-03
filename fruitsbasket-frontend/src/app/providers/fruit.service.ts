import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map, shareReplay } from 'rxjs/operators';
import { Fruit } from '../models/fruit';
import { ApiRoute } from '../shared/enum/apiRoutes.enum';
import BaseService from './common/base.service';

@Injectable({
  providedIn: 'root',
})
export class FruitService extends BaseService {
  getFruits(page?: number): Observable<Fruit[]> {
    return this.get<Fruit[]>(`${ApiRoute.FRUIT}?page=${page || 1}&ItemsByPage=${this.itemsPerPage}`).pipe(
      shareReplay(1),
      map((result: Fruit[]) => {
        return result;
      })
    );
  }

  getFruitByName(name: string, page?: number): Observable<Fruit[]> {
    page = page || 1;

    if (name) {
      return this.get<Fruit[]>(
        `${ApiRoute.NAME}?page=${page}&ItemsByPage=${this.itemsPerPage}`
      ).pipe(
        map((result: Fruit[]) => {
          return result;
        })
      );
    } 

    return this.getFruits(page);
  }

  salvar(formGroup: any, file: File) {
    file
      .arrayBuffer()
      .then((e: any) => {
        formGroup['ImagemCapa'] = btoa(new Uint8Array(e).reduce((data, byte) => data + String.fromCharCode(byte), ''));

        this.httpClient
          .post(this.API + ApiRoute.SAVE, formGroup)
          .subscribe(
            (result: any) => {
              this.toasterService.showToastSuccess(
                'Operação efetuada com sucesso.'
              );
              //this.router.navigate(['Fruit']);
            },
            (err: HttpErrorResponse) => {
              const { error } = err;
              console.log('ERROR => ' + error);
              this.toasterService.showToastError(
                'Não foi possível exetuar a operação, tente novamente mais tarde.'
              );
            }
          );
      })
      .catch((e) => console.log(e));
  }
}
