#include<stdio.h>
#include<gsl/gsl_math.h>
#include<gsl/gsl_eigen.h>
#define RND rand()/RAND_MAX
int main(int argc, char** argv){
	int n=5, max_print=8;
	if (argc > 1) n = atoi(argv[1]);
	printf("n=%i\n",n);

	gsl_matrix* a = gsl_matrix_alloc(n,n);
	gsl_matrix* v = gsl_matrix_alloc(n,n);
	gsl_vector* e = gsl_vector_alloc(n);
	gsl_eigen_symmv_workspace* w = gsl_eigen_symmv_alloc (n);
	for(int i=0;i<n;i++)for(int j=i;j<n;j++)
		{
		double aij=2*(RND-0.59);
		gsl_matrix_set(a,i,j,aij);
		gsl_matrix_set(a,j,i,aij);
		}
	gsl_eigen_symmv(a,e,v,w);
	return 0;
}
