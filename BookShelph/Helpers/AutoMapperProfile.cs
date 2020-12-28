using AutoMapper;
using BookShelph.Dtos.AudioFiles;
using BookShelph.Dtos.Authors;
using BookShelph.Dtos.Books;
using BookShelph.Dtos.Category;
using BookShelph.Dtos.Gender;
using BookShelph.Dtos.Languages;
using BookShelph.Dtos.Narrators;
using BookShelph.Dtos.Publishers;
using BookShelph.Models;
using BookShelph.ViewModels.AudioFiles;
using BookShelph.ViewModels.Authors;
using BookShelph.ViewModels.Books;
using BookShelph.ViewModels.Categories;
using BookShelph.ViewModels.Genders;
using BookShelph.ViewModels.Languages;
using BookShelph.ViewModels.Narrators;
using BookShelph.ViewModels.Publishers;

namespace BookShelph.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            string baseUrl = "http://173.248.135.167/ananse";
            string bookImageUrl = baseUrl + "/uploads/book/images/";
            string authorImageUrl = baseUrl + "/uploads/author/images/";
            string narratorImageUrl = baseUrl + "/uploads/narrator/images/";
            string categoryImageUrl = baseUrl + "/uploads/category/images/";
            string languageImageUrl = baseUrl + "/uploads/language/images/";
            string publisherImageUrl = baseUrl + "/uploads/publisher/images/";
            string audioFileUrl = baseUrl + "/uploads/audio_file/audios/";

            //Book
            CreateMap<BookCreateViewModel, Book>();
            CreateMap<Book, BookEditViewModel>()
                .ForMember(dest =>
                dest.ExistingCoverImage,
                opt => opt.MapFrom(src => src.CoverImage))
                .ReverseMap();

            CreateMap<Book, BookDto>()
                .ForMember(dest =>
                dest.CoverImage,
                opt => opt.MapFrom(src => (bookImageUrl + (src.CoverImage ?? "book_shelph.png"))));

            //Author
            CreateMap<AuthorCreateViewModel, Author>();
            CreateMap<Author, AuthorEditViewModel>()
                 .ForMember(dest =>
                dest.ExistingImage,
                opt => opt.MapFrom(src => src.Image))
                .ReverseMap();

            CreateMap<Author, AuthorDto>()
                .ForMember(dest =>
                dest.Image,
                opt => opt.MapFrom(src => (authorImageUrl + (src.Image ?? "book_shelph.png"))));

            //Narrator
            CreateMap<NarratorCreateViewModel, Narrator>();
            CreateMap<Narrator, NarratorEditViewModel>()
                .ForMember(dest =>
                dest.ExistingImage,
                opt => opt.MapFrom(src => src.Image))
                .ReverseMap();

            CreateMap<Narrator, NarratorDto>()
                .ForMember(dest =>
                dest.Image,
                opt => opt.MapFrom(src => (narratorImageUrl + (src.Image ?? "book_shelph.png"))));

            //Category
            CreateMap<CategoryCreateViewModel, Category>();
            CreateMap<Category, CategoryEditViewModel>()
                .ForMember(dest =>
                dest.ExistingImage,
                opt => opt.MapFrom(src => src.Image))
                .ReverseMap();

            CreateMap<Category, CategoryDto>()
                .ForMember(dest =>
                dest.Image,
                opt => opt.MapFrom(src => (categoryImageUrl + (src.Image ?? "book_shelph.png"))));

            //Language
            CreateMap<LanguageCreateViewModel, Language>();
            CreateMap<Language, LanguageEditViewModel>()
                .ForMember(dest =>
                dest.ExistingImage,
                opt => opt.MapFrom(src => src.Image))
                .ReverseMap();

            CreateMap<Language, LanguageDto>()
                .ForMember(dest =>
                dest.Image,
                opt => opt.MapFrom(src => (languageImageUrl + (src.Image ?? "book_shelph.png"))));

            //Publisher
            CreateMap<PublisherCreateViewModel, Publisher>();
            CreateMap<Publisher, PublisherEditViewModel>()
                .ForMember(dest =>
                dest.ExistingImage,
                opt => opt.MapFrom(src => src.Image))
                .ReverseMap();

            CreateMap<Publisher, PublisherDto>()
                .ForMember(dest =>
                dest.Image,
                opt => opt.MapFrom(src => (publisherImageUrl + (src.Image ?? "book_shelph.png"))));

            //AudioFile
            CreateMap<AudioFileCreateViewModel, AudioFile>();
            CreateMap<AudioFile, AudioFileEditViewModel>().ReverseMap();

            CreateMap<AudioFile, AudioFileDto>()
                .ForMember(dest =>
                dest.File,
                opt => opt.MapFrom(src => (audioFileUrl + (src.FilePath ?? "book_shelph.png"))));

            //Gender
            CreateMap<GenderCreateViewModel, Gender>();
            CreateMap<Gender, GenderEditViewModel>().ReverseMap();

            CreateMap<Gender, GenderDto>();

        }
    }
}
